using System;
using System.Security.Claims;

namespace Logistics.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<bool> ValidatorTokenAsync(string userId, string tokenVersionClaim);
        string GenerateTokens(IEnumerable<Claim> claims);
    }
}

