using System;
using Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.Data.Configs
{
    public class ConsigneeConfig : IEntityTypeConfiguration<Consignee>
    {
        public void Configure(EntityTypeBuilder<Consignee> builder)
        {
            builder.Property(c => c.Name).HasColumnType("varchar(20)");
            builder.Property(c => c.Phone).HasColumnType("varchar(256)");
        }
    }
}

