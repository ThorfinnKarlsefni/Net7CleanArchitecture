using System;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddClaimAsync(Role role, Claim claim)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task<IdentityResult> CreateAsync(Role role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(Role role)
        {
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<Role> FindByIdAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                throw new AggregateException("角色不存在");
            return role;
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                throw new AggregateException("角色不存在");
            return role;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            //int skipCount = (page - 1) * pageSize;
            return await _roleManager.Roles
              .ToListAsync();
            //.Skip(skipCount)
            //.Take(pageSize)
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<IList<Claim>> GetClaimsAsync(Role role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(Role role)
        {
            return await _roleManager.UpdateAsync(role);
        }
    }
}

