using System.Data.Common;
using Logistics.Domain.Entities.Identity;

namespace Logistics.Domain;

public interface IMenuRepository
{
    Task<Menu> FindMenuAsync(int id);
    Task<List<Menu>> GetMenuListAsync();
    Task<List<Menu>> GetMenuListByUserIdAsync(string userId);
    Task<ICollection<Menu>> GetMenuPathListAsync();
    Task<Menu?> GetMenuByPathAsync(string path);
    Task AddMenuAsync(Menu menu, List<string>? menuRoles);
    Task UpdateMenuAsync(int id, Menu updateMenu, List<string>? menuRoles);
    Task UpdateMenuTreeAsync(int id, int parent, Boolean dropToGap, int dropKey, int dropPosition);
    Task UpdateMenuVisibilityAsync(int id);
    Task DeleteAsync(int id);
}
