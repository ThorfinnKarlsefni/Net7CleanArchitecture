using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public UserLogin()
        {
        }
    }
}

