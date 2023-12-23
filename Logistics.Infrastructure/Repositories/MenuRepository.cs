using System.Diagnostics.CodeAnalysis;
using Logistics.Domain;
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
    public async Task<Menu> FindMenuAsync(int id)
    {
        return await _context.Menus.Include(menu => menu.MenuRoles).Where(menu => menu.Id == id).FirstAsync();
    }
    public async Task<List<Menu>> GetMenuListAsync()
    {
        // The menu list and menu tree are shared
        return await _context.Menus.OrderBy(menu => menu.Order).ToListAsync();
    }

    public async Task<List<Menu>> GetMenuListByUserIdAsync(string userId)
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
            return await GetMenuListAsync();

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

    public async Task<ICollection<Menu>> GetMenuPathListAsync()
    {
        return await _context.Menus.Where(menu => menu.ParentId != 0).ToListAsync();
    }

    public async Task<Menu?> GetMenuByPathAsync(string path)
    {
        return await _context.Menus.Where(menu => menu.Path == path).FirstOrDefaultAsync();
    }

    public async Task AddMenuAsync(Menu menu, List<string>? menuRoles)
    {
        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();
        if (menuRoles != null && menuRoles.Any())
            await AddRolesByMenu(menu.Id, menuRoles);
        await _context.SaveChangesAsync();
    }

    public async Task AddRolesByMenu(int menuId, List<string> menuRoles)
    {
        var roles = menuRoles.Select(roleId => new MenuRole
        {
            MenuId = menuId,
            RoleId = Guid.Parse(roleId),
        }).ToList();

        await _context.MenuRoles.AddRangeAsync(roles);
    }

    public async Task UpdateMenuTreeAsync(int id, int parent, Boolean dropToGap, int dropKey, int dropPosition)
    {
        var draggedMenu = await _context.Menus.Where(menu => menu.Id == id).FirstAsync();
        draggedMenu.ParentId = dropToGap ? parent : dropKey;
        draggedMenu.Order = dropPosition;
        draggedMenu.UpdateTimestamp();
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

    public async Task UpdateMenuAsync(int id, Menu updateMenu, List<string>? menuRoles)
    {
        var menu = await FindMenuAsync(id);
        menu.UpdateTimestamp();
        _context.Entry(menu).CurrentValues.SetValues(updateMenu);
        var roles = await _context.MenuRoles.Where(menuRoles => menuRoles.MenuId == id).ToListAsync();
        if (roles != null && roles.Any())
            _context.MenuRoles.RemoveRange(roles);
        if (menuRoles != null && menuRoles.Any())
            await AddRolesByMenu(id, menuRoles);
        await _context.SaveChangesAsync();
    }
}