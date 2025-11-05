using Aliance.Domain.Entities;
using Aliance.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class MissionCampaignDonationConfiguration : IEntityTypeConfiguration<MissionCampaignDonation>
{
    public void Configure(EntityTypeBuilder<MissionCampaignDonation> builder)
    {
        builder.ToTable("MissionCampaignDonation");

        builder.HasKey(mc => mc.Id);

        builder.Property(mc => mc.Guid)
            .IsRequired()
            .HasDefaultValueSql("(UUID())")
            .ValueGeneratedOnAdd();

        builder.Property(mc => mc.UserId)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasOne(mc => mc.User)
            .WithMany()
            .HasForeignKey(mc => mc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mc => mc.Campaign)
            .WithMany(c => c.Donations)
            .HasForeignKey(mc => mc.CampaignId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(ap => ap.Amount, money =>
        {
            money.Ignore(m => m.Notifications);
            money.Property(m => m.Value)
                 .HasColumnName("Cost")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
        });
    }
}
