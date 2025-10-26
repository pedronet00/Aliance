using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class WorshipTeamConfiguration : IEntityTypeConfiguration<WorshipTeam>
{
    public void Configure(EntityTypeBuilder<WorshipTeam> builder)
    {
        builder.ToTable("WorshipTeam");

        builder.HasKey(wt => wt.Id);


        builder.Property(lm => lm.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.Property(wt => wt.Guid)
            .IsRequired();

        builder.Property(wt => wt.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(wt => wt.Status)
            .IsRequired();

        // Relacionamento 1:N com WorshipTeamMembers
        builder.HasMany(wt => wt.WorshipTeamMembers)
            .WithOne(m => m.WorshipTeam)
            .HasForeignKey(m => m.WorshipTeamId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:N com WorshipTeamRehearsals
        builder.HasMany(wt => wt.WorshipTeamRehearsals)
            .WithOne(r => r.WorshipTeam)
            .HasForeignKey(r => r.WorshipTeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
