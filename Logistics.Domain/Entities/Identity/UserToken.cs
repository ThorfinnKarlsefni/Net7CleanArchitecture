using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<int>
    {
        public UserToken()
        {
        }
    }
}

