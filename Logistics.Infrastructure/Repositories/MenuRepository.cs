using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using Logistics.Domain;
using Logistics.Domain.Entities.Identity;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Logistics.Infrastructure;

public class MenuRepository : IMenuRepository
{
    private readonly XhwtDbContext _context;
    public MenuRepository(XhwtDbContext dbContext)
    {
        _context = dbContext;
    }
    public async Task<ICollection<Menu>> GetMenuTreeAsync()
    {
        return await _context.Menus.OrderByDescending(m => m.Order).ToListAsync();
    }

    public async Task<ICollection<Menu>> GetMenuListByUserIdAsync(string userId)
    {
        var roleInfo = await _context.UserRoles
            .Where(ur => ur.UserId == Guid.Parse(userId))
            .Join(
                _context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => new { RoleId = ur.RoleId, RoleName = r.Name }
            ).ToListAsync();

        var roleIds = roleInfo.Select(r => r.RoleId).ToList();
        var roleName = roleInfo.Select(r => r.RoleName).ToList();
        if (roleName.Contains("Admin"))
            return await GetMenuTreeAsync();

        var menuIdsForRole = await _context.MenuRoles
            .Where(mr => roleIds.Contains(mr.RoleId))
            .Select(mr => mr.MenuId)
            .ToListAsync();

        return await _context.Menus
            .Where(menu => menuIdsForRole.Contains(menu.Id))
            .Where(menu => menu.HideInMenu == false)
            .OrderBy(menu => menu.Order)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Menu?> GetMenuByPathAsync(string path)
    {
        return await _context.Menus.Where(menu => menu.Path == path).FirstOrDefaultAsync();
    }

    public async Task AddMenuAsync(Menu menu, List<string>? roleIds)
    {
        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();
        await AddRolesByMenu(menu.Id, roleIds);
        await _context.SaveChangesAsync();
    }

    public async Task AddRolesByMenu(int menuId, List<string>? roleIds)
    {
        if (roleIds != null && roleIds.Any())
        {
            var menuRoles = roleIds.Select(roleId => new MenuRole
            {
                MenuId = menuId,
                RoleId = Guid.Parse(roleId),
            }).ToList();

            await _context.MenuRoles.AddRangeAsync(menuRoles);
        }
    }

    public async Task UpdateMenuTreeAsync(int dragKey, int dropKey, int dropPosition)
    {
        var menu = await _context.Menus.Where(menu => menu.Id == dragKey).FirstAsync();
        menu.ParentId = dropKey;
        menu.Order = dropPosition;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMenuVisibilityAsync(int id)
    {
        var menu = await _context.Menus.Where(menu => menu.Id == id).FirstAsync();
        menu.HideInMenu = menu.HideInMenu == false ? true : false;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var menu = await _context.Menus.FindAsync(id);
        if (menu == null)
            throw new AggregateException("未找到菜单id");
        var menuRoles = await _context.MenuRoles.Where(mr => mr.MenuId == id).ToListAsync();
        if (menuRoles.Any())
        {
            _context.MenuRoles.RemoveRange(menuRoles);
        }
        _context.Menus.Remove(menu);

        await _context.SaveChangesAsync();
    }
}
