using System;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddClaimAsync(User user, Claim claim)
        {
            return await _userManager.AddClaimAsync(user, claim);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string password)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {

            var res = await _userManager.CheckPasswordAsync(user, password);
            if (!res)
                throw new AggregateException("用户名或密码错误");
            return true;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new AggregateException("用户不存在");
            return user;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new AggregateException("用户不存在");
            return user;
        }

        public async Task<User> FindByUserIdAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstAsync(user => user.UserId == userId);
            if (user == null)
                throw new AggregateException("用户不存在");
            return user;
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<User?> GetByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}

