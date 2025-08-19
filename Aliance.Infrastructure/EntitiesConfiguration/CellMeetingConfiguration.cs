using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class CellMeetingConfiguration : IEntityTypeConfiguration<CellMeeting>
{
    public void Configure(EntityTypeBuilder<CellMeeting> builder)
    {
        builder.ToTable("CellMeeting");

        builder.HasKey(cm => cm.Id);

        builder.HasOne(cm => cm.Cell)
            .WithMany(c => c.CellMeetings)
            .HasForeignKey(cm => cm.CellId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.Location)
            .WithMany()
            .HasForeignKey(cm => cm.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cm => cm.Leader)
            .WithMany()
            .HasForeignKey(cm => cm.LeaderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(cm => cm.Theme)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cm => cm.Date)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(cm => cm.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

    }
}
