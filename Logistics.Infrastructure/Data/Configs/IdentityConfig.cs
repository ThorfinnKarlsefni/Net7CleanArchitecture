using System;
using Logistics.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.Data.Configs
{
    public class IdentityConfig
    {
        public class UserConfig : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("Users").Property(u => u.TokenVersion).HasDefaultValue(0);
            }
        }

        public class RoleConfig : IEntityTypeConfiguration<Role>
        {
            public void Configure(EntityTypeBuilder<Role> builder)
            {
                builder.ToTable("Roles");
            }
        }

        public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
        {
            public void Configure(EntityTypeBuilder<UserRole> builder)
            {
                builder.HasKey(u => new { u.UserId, u.RoleId });
                builder.ToTable("UserRole");
            }
        }

        public class UserLoginConfig : IEntityTypeConfiguration<UserLogin>
        {
            public void Configure(EntityTypeBuilder<UserLogin> builder)
            {
                builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });
                builder.ToTable("UserLogin");
            }
        }

        public class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
        {
            public void Configure(EntityTypeBuilder<UserClaim> builder)
            {
                builder.ToTable("UserClaim");
            }
        }

        public class RoleClaimConfig : IEntityTypeConfiguration<RoleClaim>
        {
            public void Configure(EntityTypeBuilder<RoleClaim> builder)
            {
                builder.ToTable("RoleClaim");
            }
        }

        public class UserTokenConfig : IEntityTypeConfiguration<UserToken>
        {
            public void Configure(EntityTypeBuilder<UserToken> builder)
            {
                builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
                builder.ToTable("UserToken");
            }
        }
    }

}

