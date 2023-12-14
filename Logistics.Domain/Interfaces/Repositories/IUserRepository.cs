using System;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddClaimAsync(User user, Claim claim);

        //Task<User> FindByIdAsync(Guid id);
        Task<User> FindByIdAsync(string id);

        Task<User> FindByNameAsync(string userName);

        Task<User?> GetByNameAsync(string userName);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<IdentityResult> CreateAsync(User user, string password);

        Task<IList<string>> GetRolesAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string password);

        Task<IdentityResult> UpdateAsync(User user);

        Task<IdentityResult> AddToRoleAsync(User user, string role);
    }
}

