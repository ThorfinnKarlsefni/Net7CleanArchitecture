using System;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> CreateAsync(string roleName)
        {
            if (await _roleRepository.GetByNameAsync(roleName) != null)
                throw new AggregateException("角色名称已存在");
            var role = new Role { Name = roleName };
            return await _roleRepository.CreateAsync(role).ConfigureAwait(false);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleRepository.FindByIdAsync(roleId).ConfigureAwait(false);
            return await _roleRepository.DeleteAsync(role).ConfigureAwait(false);
        }

        public async Task<Role?> FindByIdAsync(string roleId)
        {
            return await _roleRepository.FindByIdAsync(roleId).ConfigureAwait(false);
        }

        public async Task<IdentityResult> UpdateRoleAsync(string roleId, string roleName)
        {
            var role = await _roleRepository.FindByIdAsync(roleId).ConfigureAwait(false);
            role.Name = roleName;
            return await _roleRepository.UpdateAsync(role).ConfigureAwait(false);
        }

    }
}

