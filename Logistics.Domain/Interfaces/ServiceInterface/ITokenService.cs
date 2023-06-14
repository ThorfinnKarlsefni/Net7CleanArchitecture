using System;
using System.Security.Claims;

namespace Logistics.Domain.Interfaces.ServiceInterface
{
    public interface ITokenService
    {
        string BuildToken(IEnumerable<Claim> claims);
    }
}

