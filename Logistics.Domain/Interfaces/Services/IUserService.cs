using System;
using Logistics.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> GenerateTokensAsync(User user);
        Task<IdentityResult> UpdateUserAsync(Guid userId, string userName);
        Task<IdentityResult> ResetPasswordAsync(Guid userId, string oldPassword, string newPassword);
        Task<IdentityResult> AddToRoleAsync(Guid userId, string roleId);
        Task<(User user, IList<string> roles)> GetUserInfo(Guid userId);
    }
}

