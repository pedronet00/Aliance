using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expense");

        builder.HasKey(e => e.Id);

        builder.Property(lm => lm.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.HasOne(e => e.AccountPayable)
            .WithMany(ap => ap.Expenses) 
            .HasForeignKey(e => e.AccountPayableId)
            .OnDelete(DeleteBehavior.SetNull) 
            .IsRequired(false);

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Date)
            .IsRequired()
            .HasColumnType("datetime");
    }
}
