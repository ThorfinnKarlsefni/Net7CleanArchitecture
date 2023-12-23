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

    public async Task<List<Permission>> GetPermissionsAsync()
    {
        return await _context.Permission.ToListAsync();
    }
}
