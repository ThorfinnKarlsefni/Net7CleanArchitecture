using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<User?> FindByPhoneNumberAsync(string phoneNum)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNum);
        }

        public Task<User?> FindByIdAsync(Guid userId)
        {
            return _userManager.FindByIdAsync(userId.ToString());
        }

        public Task<User?> FindByNameAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }
        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> AccessFailedAsync(User user)
        {
            return _userManager.AccessFailedAsync(user);
        }

        public async Task<SignInResult> ChangePhoneNumAsync(Guid userId, string phoneNum, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new ArgumentException($"{userId}的用户不存在");
            }
            var changeResult = await _userManager.ChangePhoneNumberAsync(user, phoneNum, token);
            if (!changeResult.Succeeded)
            {
                await _userManager.AccessFailedAsync(user);
                string errMsg = changeResult.Errors.First().Description;
                _logger.LogWarning($"{phoneNum}ChangePhoneNumberAsync失败，错误信息{errMsg}");
                return SignInResult.Failed;
            }
            else
            {
                await ConfirmPhoneNumberAsync(user.UserId);//确认手机号
                return SignInResult.Success;
            }
        }
        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, string password)
        {
            if (password.Length < 6)
            {
                IdentityError err = new IdentityError();
                err.Code = "Password Invalid";
                err.Description = "密码长度不能少于6";
                return IdentityResult.Failed(err);
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return IdentityResult.Failed();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPwdResult = await _userManager.ResetPasswordAsync(user, token, password);
            return resetPwdResult;


        }

        public Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber)
        {
            return _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                Role role = new Role { Name = roleName };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded == false)
                {
                    return result;
                }
            }
            return await _userManager.AddToRoleAsync(user, roleName);
        }
        /// <summary>
        /// 尝试登录，如果lockoutOnFailure为true，则登录失败还会自动进行lockout计数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<SignInResult> CheckForSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            if (await _userManager.IsLockedOutAsync(user))
            {
                return SignInResult.LockedOut;
            }
            var success = await _userManager.CheckPasswordAsync(user, password);
            if (success)
            {
                return SignInResult.Success;
            }
            else
            {
                if (lockoutOnFailure)
                {
                    var r = await AccessFailedAsync(user);
                    if (!r.Succeeded)
                    {
                        throw new ApplicationException("AccessFailed failed");
                    }
                }
                return SignInResult.Failed;
            }
        }
        public async Task ConfirmPhoneNumberAsync(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new ArgumentException($"用户找不到，id={id}", nameof(id));
            }
            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdatePhoneNumberAsync(Guid id, string phoneNum)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new ArgumentException($"用户找不到，id={id}", nameof(id));
            }
            user.PhoneNumber = phoneNum;
            await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveUserAsync(Guid id)
        {
            var user = await FindByIdAsync(id);
            //var userLoginStore = IUserLoginStore < User > this.store;
            //var noneCT = default(CancellationToken);
            ////一定要删除aspnetuserlogins表中的数据，否则再次用这个外部登录登录的话
            ////就会报错：The instance of entity type 'IdentityUserLogin<Guid>' cannot be tracked because another instance with the same key value for {'LoginProvider', 'ProviderKey'} is already being tracked.
            ////而且要先删除aspnetuserlogins数据，再软删除User
            //var logins = await userLoginStore.GetLoginsAsync(user, noneCT);
            //foreach (var login in logins)
            //{
            //    await userLoginStore.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey, noneCT);
            //}
            //user.SoftDelete();
            if (user == null)
                return IdentityResult.Failed();
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        private static IdentityResult ErrorResult(string msg)
        {
            IdentityError idError = new IdentityError { Description = msg };
            return IdentityResult.Failed(idError);
        }

        public async Task<(IdentityResult, User?, string? password)> AddAdminUserAsync(string userName, string phoneNum)
        {
            if (await FindByNameAsync(userName) != null)
            {
                return (ErrorResult($"已经存在用户名{userName}"), null, null);
            }
            if (await FindByPhoneNumberAsync(phoneNum) != null)
            {
                return (ErrorResult($"已经存在手机号{phoneNum}"), null, null);
            }
            User user = new User(userName);
            user.PhoneNumber = phoneNum;
            user.PhoneNumberConfirmed = true;
            string password = GeneratePassword();
            var result = await CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return (result, null, null);
            }
            result = await AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                return (result, null, null);
            }
            return (IdentityResult.Success, user, password);
        }

        public async Task<(IdentityResult, User?, string? password)> ResetPasswordAsync(Guid id)
        {
            var user = await FindByIdAsync(id);
            if (user == null)
            {
                return (ErrorResult("用户没找到"), null, null);
            }
            string password = GeneratePassword();
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                return (result, null, null);
            }
            return (IdentityResult.Success, user, password);
        }

        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;
            int length = options.RequiredLength;
            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;
            StringBuilder password = new StringBuilder();
            Random random = new Random();
            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);
                password.Append(c);
                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));
            return password.ToString();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userManager.Users.Where(user => user.DeletedAt == null).ToListAsync();
        }
    }
}

