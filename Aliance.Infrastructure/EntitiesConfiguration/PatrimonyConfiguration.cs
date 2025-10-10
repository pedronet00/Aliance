using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class PatrimonyConfiguration : IEntityTypeConfiguration<Patrimony>
{
    public void Configure(EntityTypeBuilder<Patrimony> builder)
    {
        builder.ToTable("Patrimony");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Guid)
        .IsRequired()
        .HasDefaultValueSql("(UUID())") // gera automaticamente no MySQL
        .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(p => p.UnitValue)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.TotalValue)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.AcquisitionDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(p => p.Condition)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(p => p.Church)
            .WithMany(c => c.Patrimonies)
            .HasForeignKey(p => p.ChurchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
