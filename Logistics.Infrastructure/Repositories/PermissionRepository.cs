using System.Net.NetworkInformation;
using Logistics.Domain;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure;

public class PermissionRepository : IPermissionRepository
{
    private readonly XhwtDbContext _context;
    public PermissionRepository(XhwtDbContext context)
    {
        _context = context;
    }

    public async Task AddPermissionAsync(Permission permission)
    {
        await _context.Permission.AddAsync(permission);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePermissionAsync(int id)
    {
        var permission = await _context.Permission.Where(p => p.Id == id).FirstAsync();
        _context.Permission.Remove(permission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Permission>> GetPermissionsAsync()
    {
        return await _context.Permission.ToListAsync();
    }

    public async Task UpdatePermissionAsync(Permission permission)
    {
        _context.Permission.Update(permission);
        await _context.SaveChangesAsync();
    }
}
