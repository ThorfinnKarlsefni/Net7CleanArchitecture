﻿using System;
using Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.Data.Configs
{
    public class BankConfig : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.Property(b => b.BankName).HasColumnType("varchar(20)");
            builder.Property(b => b.CreatedAt).HasColumnType("timestamp");
            builder.Property(b => b.UpdatedAt).HasColumnType("timestamp");
        }
    }
}