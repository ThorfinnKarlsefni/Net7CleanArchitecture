using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Logistics.Infrastructure.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IJwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        public TokenService(IAppSettings appSettings, IUserRepository userRepository)
        {
            _jwtSettings = appSettings;
            _userRepository = userRepository;
        }

        public string GenerateTokens(IEnumerable<Claim> claims)
        {
            TimeSpan ExpiryDuration = TimeSpan.FromSeconds(_jwtSettings.DurationInMinutes);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<bool> ValidatorTokenAsync(string userId, string tokenVersionClaim)
        {
            var user = await _userRepository.FindByUserIdAsync(Guid.Parse(userId)).ConfigureAwait(false);
            if (user?.TokenVersion.ToString() == tokenVersionClaim)
                return true;
            return false;
        }
    }
}

