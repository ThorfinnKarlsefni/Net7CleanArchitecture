using System;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.RepositoryInterface;
using Logistics.Domain.Interfaces.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Logistics.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JWTOptions> _optJWT;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IOptions<JWTOptions> options)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _optJWT = options;
        }

        private async Task<SignInResult> CheckUserNameAndPwdAsync(string userName, string password)
        {
            var user = await _userRepository.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null)
                return SignInResult.Failed;
            var result = await _userRepository.CheckForSignInAsync(user, password, true).ConfigureAwait(false);
            return result;
        }

        private async Task<SignInResult> CheckPhoneNumAndPwdAsync(string phoneNum, string password)
        {
            var user = await _userRepository.FindByPhoneNumberAsync(phoneNum).ConfigureAwait(false);
            if (user == null)
                return SignInResult.Failed;
            var result = await _userRepository.CheckForSignInAsync(user, password, true).ConfigureAwait(false);
            return result;
        }

        public async Task<(SignInResult Result, string? Token)> LoginByUserPhoneAndPwdAsync(string phoneNumber, string password)
        {
            var checkResult = await CheckPhoneNumAndPwdAsync(phoneNumber, password).ConfigureAwait(false);
            if (!checkResult.Succeeded)
                return (checkResult, null);

            var user = await _userRepository.FindByPhoneNumberAsync(phoneNumber).ConfigureAwait(false);
            string token = await BuildTokenAsync(user);
            return (SignInResult.Success, token);
        }

        public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
        {
            var checkResult = await CheckUserNameAndPwdAsync(userName, password).ConfigureAwait(false);
            if (!checkResult.Succeeded)
                return (checkResult, null);

            var user = await _userRepository.FindByNameAsync(userName).ConfigureAwait(false);
            string token = await BuildTokenAsync(user);
            return (SignInResult.Success, token);
        }

        private async Task<string> BuildTokenAsync(User user)
        {
            var roles = await _userRepository.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return _tokenService.BuildToken(claims, _optJWT.Value);
        }
    }
}

