using Aliance.Domain.Entities;
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

        builder.Property(lm => lm.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.HasOne(i => i.AccountReceivable)
            .WithMany(ar => ar.Incomes) 
            .HasForeignKey(i => i.AccountReceivableId)
            .OnDelete(DeleteBehavior.SetNull) 
            .IsRequired(false);

        builder.OwnsOne(ap => ap.Amount, money =>
        {
            money.Ignore(m => m.Notifications);
            money.Property(m => m.Value)
                 .HasColumnName("Amount")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
        });

        builder.Property(i => i.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Date)
            .IsRequired()
            .HasColumnType("datetime");
    }
}
