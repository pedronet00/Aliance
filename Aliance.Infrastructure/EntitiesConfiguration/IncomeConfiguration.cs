﻿using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Income");

        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.AccountReceivable)
            .WithMany(ar => ar.Incomes) 
            .HasForeignKey(i => i.AccountReceivableId)
            .OnDelete(DeleteBehavior.SetNull) 
            .IsRequired(false);

        builder.Property(i => i.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Date)
            .IsRequired()
            .HasColumnType("datetime");
    }
}
