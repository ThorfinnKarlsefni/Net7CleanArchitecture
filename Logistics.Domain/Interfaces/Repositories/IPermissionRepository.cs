namespace Logistics.Domain;

public interface IPermissionRepository
{
    Task AddPermissionAsync(Permission permission);
    Task<List<Permission>> GetPermissionsAsync();
}
