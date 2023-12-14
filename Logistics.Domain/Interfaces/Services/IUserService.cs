using System;
using Logistics.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> GenerateTokensAsync(User user);
        Task<IdentityResult> UpdateUserAsync(string userId, string userName);
        Task<IdentityResult> ResetPasswordAsync(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> AddToRoleAsync(string userId, string roleId);
        Task<(User user, IList<string> roles)> GetUserInfo(string userId);
    }
}

