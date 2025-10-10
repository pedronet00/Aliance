using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class LeadershipMeetingConfiguration : IEntityTypeConfiguration<LeadershipMeetings>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LeadershipMeetings> builder)
    {
        builder.ToTable("LeadershipMeetings");

        builder.HasKey(lm => lm.Id);

        builder.Property(lm => lm.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.Property(lm => lm.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(lm => lm.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(lm => lm.MeetingDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(lm => lm.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}
