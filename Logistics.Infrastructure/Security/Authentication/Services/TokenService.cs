using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logistics.Infrastructure.Security.Authentication;
using Logistics.Domain.Interfaces.ServiceInterface;
using Logistics.Infrastructure.Settings;
using System.Security.Cryptography;

namespace Logistics.Infrastructure.Security.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJwtSettings _jwtSettings;

        public TokenService(IAppSettings appSettings)
        {
            _jwtSettings = appSettings;
        }

        public string BuildToken(IEnumerable<Claim> claims)
        {
            TimeSpan ExpiryDuration = TimeSpan.FromSeconds(_jwtSettings.DurationInMinutes);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDecriptor = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDecriptor);
        }

        public string GenerateRefreshToken()
        {
            var randomNumer = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumer);
                return Convert.ToBase64String(randomNumer);
            }
        }
    }
}

