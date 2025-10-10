using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class PastoralVisitConfiguration : IEntityTypeConfiguration<PastoralVisit>
{
    public void Configure(EntityTypeBuilder<PastoralVisit> builder)
    {
        builder.ToTable("PastoralVisit");

        builder.HasKey(pv => pv.Id);

        builder.Property(pv => pv.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.Property(pv => pv.VisitDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(pv => pv.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(pv => pv.VisitedMember)
            .WithMany()
            .HasForeignKey(pv => pv.VisitedMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.Pastor)
            .WithMany() 
            .HasForeignKey(pv => pv.PastorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.Church)
            .WithMany(c => c.PastoralVisits)
            .HasForeignKey(pv => pv.ChurchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pv => pv.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}
