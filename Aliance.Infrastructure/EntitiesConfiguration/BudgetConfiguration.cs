using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budget");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(ap => ap.TotalAmount, money =>
        {
            money.Ignore(m => m.Notifications);
            money.Property(m => m.Value)
                 .HasColumnName("TotalAmount")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
        });

        builder.Property(b => b.StartDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(b => b.EndDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(b => b.CostCenter)
            .WithMany(cc => cc.Budget)
            .HasForeignKey(b => b.CostCenterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
