using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration
{
    public class CellConfiguration : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            builder.ToTable("Cell");

            builder.HasKey(c => c.Id);

            builder.Property(lm => lm.Guid)
                .HasDefaultValueSql("(UUID())");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CreatedAt)
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime")
                .ValueGeneratedOnUpdate();

            builder.HasOne(c => c.Location)
                .WithMany()
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Leader)
                .WithMany()
                .HasForeignKey(c => c.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.MeetingDay)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(c => c.CellBanner)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(c => c.Church)
                .WithMany(ch => ch.Cells)
                .HasForeignKey(c => c.ChurchId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
