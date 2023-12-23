using Logistics.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(p => p.Name).HasColumnType("varchar(50)");
        builder.Property(p => p.HttpMethod).HasColumnType("varchar(191)");
        builder.Property(p => p.CreatedAt).HasColumnType("timestamp");
        builder.Property(p => p.UpdatedAt).HasColumnType("timestamp");
    }
}
