using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Guid)
        .IsRequired()
        .HasDefaultValueSql("(UUID())") // gera automaticamente no MySQL
        .ValueGeneratedOnAdd();

        builder.Property(s => s.Date)
            .IsRequired()
            .HasColumnType("datetime");

        builder.HasOne(s => s.Location)
            .WithMany()
            .HasForeignKey(s => s.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(s => s.Church)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.ChurchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
