using System;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepostiory;
        private readonly ITokenService _tokenService;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository repository, ITokenService tokenService, IRoleRepository roleRepository)
        {
            _userRepostiory = repository;
            _tokenService = tokenService;
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> AddToRoleAsync(string userId, string roleId)
        {
            var user = await _userRepostiory.FindByIdAsync(userId).ConfigureAwait(false);
            var role = await _roleRepository.FindByIdAsync(roleId).ConfigureAwait(false);
            if (string.IsNullOrEmpty(role?.Name))
                throw new AggregateException("角色名称为空");
            return await _userRepostiory.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
        }

        public async Task<string> GenerateTokensAsync(User user)
        {
            var roles = await _userRepostiory.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            user.TokenVersionIncrement();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim("token_version", user.TokenVersion.ToString()));
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            await _userRepostiory.UpdateAsync(user);
            return _tokenService.GenerateTokens(claims);
        }

        public async Task<(User user, IList<string> roles)> GetUserInfo(string userId)
        {
            var user = await _userRepostiory.FindByIdAsync(userId).ConfigureAwait(false);
            var roles = await _userRepostiory.GetRolesAsync(user);
            return (user, roles);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userRepostiory.FindByIdAsync(userId);
            await _userRepostiory.CheckPasswordAsync(user, oldPassword);
            return await _userRepostiory.ResetPasswordAsync(user, newPassword);
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, string userName)
        {
            var user = await _userRepostiory.FindByIdAsync(userId).ConfigureAwait(false);
            user.UserName = userName;
            return await _userRepostiory.UpdateAsync(user).ConfigureAwait(false);
        }
    }
}

