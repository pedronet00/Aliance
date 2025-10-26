using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class WorshipTeamMemberConfiguration : IEntityTypeConfiguration<WorshipTeamMember>
{
    public void Configure(EntityTypeBuilder<WorshipTeamMember> builder)
    {
        builder.ToTable("WorshipTeamMember");

        builder.HasKey(wtm => wtm.Id);


        builder.Property(lm => lm.Guid)
            .HasDefaultValueSql("(UUID())");

        builder.Property(wtm => wtm.Status)
            .IsRequired();

        // Relacionamento com User (ApplicationUser)
        builder.HasOne(wtm => wtm.User)
            .WithMany() // se ApplicationUser não tiver ICollection<WorshipTeamMember>
            .HasForeignKey(wtm => wtm.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com WorshipTeam
        builder.HasOne(wtm => wtm.WorshipTeam)
            .WithMany(wt => wt.WorshipTeamMembers)
            .HasForeignKey(wtm => wtm.WorshipTeamId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índice único para evitar duplicidade de membro no mesmo time
        builder.HasIndex(wtm => new { wtm.UserId, wtm.WorshipTeamId })
            .IsUnique();
    }
}
