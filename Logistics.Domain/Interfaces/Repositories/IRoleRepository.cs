using System;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRoles();

        Task<IdentityResult> AddClaimAsync(Role role, Claim claim);

        Task<IdentityResult> CreateAsync(Role role);

        Task<IdentityResult> UpdateAsync(Role role);

        Task<IdentityResult> DeleteAsync(Role role);

        Task<Role> FindByIdAsync(string roleId);

        Task<Role> FindByNameAsync(string roleName);

        Task<IList<Claim>> GetClaimsAsync(Role role);

        Task<Role?> GetByNameAsync(string roleName);
    }
}

