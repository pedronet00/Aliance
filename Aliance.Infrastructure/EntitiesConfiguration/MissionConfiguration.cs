using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    public void Configure(EntityTypeBuilder<Mission> builder)
    {
        builder.ToTable("Mission");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.City)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(m => m.State)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.Status)
            .IsRequired();

        builder.HasOne(m => m.Church)
            .WithMany(c => c.Missions)
            .HasForeignKey(m => m.ChurchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
