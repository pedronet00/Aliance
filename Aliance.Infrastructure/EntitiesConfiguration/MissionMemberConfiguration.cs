using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class MissionMemberConfiguration : IEntityTypeConfiguration<MissionMember>
{
    public void Configure(EntityTypeBuilder<MissionMember> builder)
    {
        builder.ToTable("MissionMember");

        builder.HasKey(mm => mm.Id);

        builder.HasOne(mm => mm.Mission)
            .WithMany(m => m.MissionMembers)
            .HasForeignKey(mm => mm.MissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mm => mm.Member)
            .WithMany()
            .HasForeignKey(mm => mm.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(mm => mm.Status)
            .IsRequired();



    }
}
