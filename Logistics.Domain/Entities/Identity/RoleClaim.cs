using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public RoleClaim()
        {
        }
    }
}

