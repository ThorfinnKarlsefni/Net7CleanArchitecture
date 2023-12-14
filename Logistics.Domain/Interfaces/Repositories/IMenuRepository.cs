using System.Data.Common;
using Logistics.Domain.Entities.Identity;

namespace Logistics.Domain;

public interface IMenuRepository
{
    Task<ICollection<Menu>> GetMenuTreeAsync();
    Task<ICollection<Menu>> GetMenuListByUserIdAsync(string userId);
    Task<Menu?> GetMenuByPathAsync(string path);
    Task AddMenuAsync(Menu menu, List<string>? rolesId);
    Task UpdateMenuTreeAsync(int dragKey, int dropKey, int dropPosition);
    Task UpdateMenuVisibilityAsync(int id);
    Task DeleteAsync(int id);
}