using System;
using System.Security.Claims;

namespace Logistics.Domain.Interfaces.ServiceInterface
{
    public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpireSeconds { get; set; }
    }

    public interface ITokenService
    {
        string BuildToken(IEnumerable<Claim> claims, JWTOptions options);
    }
}

