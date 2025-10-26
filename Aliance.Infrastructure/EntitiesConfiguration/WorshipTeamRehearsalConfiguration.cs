using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.Configurations;

public class WorshipTeamRehearsalConfiguration : IEntityTypeConfiguration<WorshipTeamRehearsal>
{
    public void Configure(EntityTypeBuilder<WorshipTeamRehearsal> builder)
    {
        builder.ToTable("WorshipTeamRehearsals");

        builder.HasKey(r => r.Id);


        builder.Property(lm => lm.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.Property(r => r.Guid)
            .IsRequired();

        builder.Property(r => r.RehearsalDate)
            .IsRequired();

        builder.Property(r => r.Status)
            .IsRequired();

        builder.HasOne(r => r.WorshipTeam)
            .WithMany(w => w.WorshipTeamRehearsals)
            .HasForeignKey(r => r.WorshipTeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
