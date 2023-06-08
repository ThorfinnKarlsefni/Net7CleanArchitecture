using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<long>
    {
        public UserLogin()
        {
        }
    }
}

