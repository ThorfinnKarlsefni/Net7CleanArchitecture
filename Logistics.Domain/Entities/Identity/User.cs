using System;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Int64 TokenVersion { get; private set; }

        public ICollection<Waybill> CreatorWaybills { get; } = new List<Waybill>();
        public ICollection<Waybill> BargainedWaybills { get; } = new List<Waybill>();

        public User(string userName) : base(userName)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public void TokenVersionIncrement()
        {
            TokenVersion++;
        }
    }
}

