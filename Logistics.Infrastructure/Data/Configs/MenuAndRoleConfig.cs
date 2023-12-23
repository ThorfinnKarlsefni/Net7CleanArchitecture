using Logistics.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure;

public class MenuAndRoleConfig
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(m => m.Name).HasColumnType("varchar(50)");
            builder.Property(m => m.Icon).HasColumnType("varchar(50)");
            builder.Property(m => m.Path).HasColumnType("varchar(50)");
            builder.Property(m => m.Redirect).HasColumnType("varchar(50)");
            builder.Property(m => m.Component).HasColumnType("varchar(50)");
            builder.Property(m => m.CreatedAt).HasColumnType("timestamp");
            builder.Property(m => m.UpdatedAt).HasColumnType("timestamp");
            builder.Ignore(m => m.Children);
        }
    }

    public class MenuRolesConfig : IEntityTypeConfiguration<MenuRole>
    {
        public void Configure(EntityTypeBuilder<MenuRole> builder)
        {
            builder.Property(m => m.CreatedAt).HasColumnType("timestamp");
            builder.Property(m => m.UpdatedAt).HasColumnType("timestamp");
        }
    }
}