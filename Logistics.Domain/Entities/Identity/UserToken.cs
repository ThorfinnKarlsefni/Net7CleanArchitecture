using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<long>
    {
        public UserToken()
        {
        }
    }
}

