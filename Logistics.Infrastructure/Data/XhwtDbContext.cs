using System;
using Logistics.Domain;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Data
{
    public class XhwtDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public XhwtDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Waybill> Waybills { get; set; }
        public DbSet<Consignee> Consignees { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            DataSeed.Seed(builder);
        }
    }
}

