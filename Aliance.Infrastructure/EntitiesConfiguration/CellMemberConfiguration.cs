using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class CellMemberConfiguration : IEntityTypeConfiguration<CellMember>
{
    public void Configure(EntityTypeBuilder<CellMember> builder)
    {
        builder.ToTable("CellMember");

        builder.HasKey(cm => cm.Id);

        builder.Property(cm => cm.Guid)
            .IsRequired();

        // Relacionamento via chave alternativa (Guid)
        builder.HasOne(cm => cm.Cell)
            .WithMany(c => c.CellMembers)
            .HasForeignKey(cm => cm.CellGuid)
            .HasPrincipalKey(c => c.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.User)
            .WithMany()
            .HasForeignKey(cm => cm.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(cm => cm.JoinedDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(cm => cm.Status)
            .IsRequired();
    }
}
