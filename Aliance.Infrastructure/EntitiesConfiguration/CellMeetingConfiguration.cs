using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class CellMeetingConfiguration : IEntityTypeConfiguration<CellMeeting>
{
    public void Configure(EntityTypeBuilder<CellMeeting> builder)
    {
        builder.ToTable("CellMeeting");

        // Chave primária
        builder.HasKey(cm => cm.Id);

        // GUID default
        builder.Property(cm => cm.Guid)
            .HasDefaultValueSql("(UUID())")
            .IsRequired();

        // Relacionamentos via GUID
        builder.HasOne(cm => cm.Cell)
            .WithMany(c => c.CellMeetings)
            .HasPrincipalKey(c => c.Guid)
            .HasForeignKey(cm => cm.CellGuid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.Location)
            .WithMany()
            .HasPrincipalKey(l => l.Guid)
            .HasForeignKey(cm => cm.LocationGuid)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cm => cm.Leader)
            .WithMany()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(cm => cm.LeaderGuid)
            .OnDelete(DeleteBehavior.Restrict);

        // Campos obrigatórios
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
