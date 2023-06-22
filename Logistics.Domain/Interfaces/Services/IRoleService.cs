using System;
using Logistics.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IRoleService
    {
        Task<Role?> FindByIdAsync(string roleId);
        Task<IdentityResult> CreateAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleId);
        Task<IdentityResult> UpdateRoleAsync(string Id, string roleName);
    }
}

