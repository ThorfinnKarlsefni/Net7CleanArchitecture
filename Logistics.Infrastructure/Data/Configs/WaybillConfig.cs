using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.Data.Configs
{
    public class WaybillConfig : IEntityTypeConfiguration<Waybill>
    {
        public void Configure(EntityTypeBuilder<Waybill> builder)
        {
            builder.HasKey(w => w.WaybillId);
            builder.HasOne<User>().WithMany(u => u.BargainedWaybills).HasForeignKey(w => w.BargainerId);
            builder.HasOne<User>().WithMany(u => u.CreatorWaybills).HasForeignKey(w => w.CreatorId);
            builder.Property(w => w.Address).HasColumnType("varchar(256)");
            builder.Property(w => w.CargoName).HasColumnType("varchar(20)");
            builder.Property(w => w.CargoId).HasColumnType("varchar(256)");
            builder.Property(w => w.Remarks).HasColumnType("varchar(256)");
            builder.Property(w => w.UpdatedAt).HasColumnType("timestamp");
            builder.Property(w => w.CreatedAt).HasColumnType("timestamp");
            builder.Property(w => w.DeletedAt).HasColumnType("timestamp");
            builder.Property(w => w.PayAt).HasColumnType("timestamp");
        }
    }
}

