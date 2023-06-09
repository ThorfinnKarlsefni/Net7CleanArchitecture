using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class User : IdentityUser<long>
    {
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public User(string userName) : base(userName)
        {
            UserId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
