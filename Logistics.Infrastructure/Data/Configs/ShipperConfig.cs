using System;
using Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.Data.Configs
{
    public class ShipperConfig : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.Property(s => s.Name).HasColumnType("varchar(20)");
            builder.Property(s => s.Phone).HasColumnType("varchar(256)");
            builder.Property(s => s.CreatedAt).HasColumnType("timestamp");
            builder.Property(s => s.CreatedAt).HasColumnType("timestamp");
            builder.Property(s => s.UpdatedAt).HasColumnType("timestamp");
            builder.Property(s => s.DeletedAt).HasColumnType("timestamp");
        }
    }
}

