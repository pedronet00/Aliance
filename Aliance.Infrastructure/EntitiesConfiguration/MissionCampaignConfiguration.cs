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
    public class MissionCampaignConfiguration : IEntityTypeConfiguration<MissionCampaign>
    {
        public void Configure(EntityTypeBuilder<MissionCampaign> builder)
        {
            builder.ToTable("MissionCampaign");

            builder.HasKey(mc => mc.Id);

            builder.Property(mc => mc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(mc => mc.Type)
                .IsRequired();

            builder.Property(mc => mc.StartDate)
                .IsRequired();

            builder.Property(mc => mc.EndDate)
                .IsRequired();

            builder.Property(mc => mc.TargetAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(mc => mc.CollectedAmount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(mc => mc.Church)
                .WithMany(c => c.MissionCampaigns)
                .HasForeignKey(mc => mc.ChurchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
