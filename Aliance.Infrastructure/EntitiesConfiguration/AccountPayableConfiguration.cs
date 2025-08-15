﻿using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class AccountPayableConfiguration : IEntityTypeConfiguration<AccountPayable>
{
    public void Configure(EntityTypeBuilder<AccountPayable> builder)
    {
        builder.ToTable("AccountPayable");

        builder.HasKey(ap => ap.Id);

        builder.Property(ap => ap.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ap => ap.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ap => ap.DueDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(ap => ap.PaymentDate)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(ap => ap.AccountStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(ap => ap.CostCenter)
            .WithMany(cc => cc.AccountPayable)
            .HasForeignKey(ap => ap.CostCenterId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
