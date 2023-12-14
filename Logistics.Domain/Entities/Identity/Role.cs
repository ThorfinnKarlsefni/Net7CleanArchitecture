using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Role()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

