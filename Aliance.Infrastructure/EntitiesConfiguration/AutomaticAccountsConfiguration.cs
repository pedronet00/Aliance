using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class AutomaticAccountsConfiguration : IEntityTypeConfiguration<AutomaticAccounts>
{
    public void Configure(EntityTypeBuilder<AutomaticAccounts> builder)
    {
        builder.ToTable("AutomaticAccounts");

        builder.HasKey(ar => ar.Id);

        builder.Property(p => p.Guid)
        .IsRequired()
        .HasDefaultValueSql("(UUID())") 
        .ValueGeneratedOnAdd();

        builder.Property(ar => ar.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(ap => ap.Amount, money =>
        {
            money.Ignore(m => m.Notifications);
            money.Property(m => m.Value)
                 .HasColumnName("Amount")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
        });

        builder.Property(ar => ar.DueDay)
            .IsRequired();

        builder.Property(ar => ar.AccountStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(ar => ar.CostCenter)
            .WithMany(cc => cc.AutomaticAccounts)
            .HasForeignKey(ar => ar.CostCenterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
