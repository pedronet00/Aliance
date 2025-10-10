using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class AccountReceivableConfiguration : IEntityTypeConfiguration<AccountReceivable>
{
    public void Configure(EntityTypeBuilder<AccountReceivable> builder)
    {
        builder.ToTable("AccountReceivable");

        builder.HasKey(ar => ar.Id);

        builder.Property(p => p.Guid)
        .IsRequired()
        .HasDefaultValueSql("(UUID())") // gera automaticamente no MySQL
        .ValueGeneratedOnAdd();

        builder.Property(ar => ar.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ar => ar.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ar => ar.DueDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(ar => ar.PaymentDate)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(ar => ar.AccountStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(ar => ar.CostCenter)
            .WithMany(cc => cc.AccountReceivable)
            .HasForeignKey(ar => ar.CostCenterId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
