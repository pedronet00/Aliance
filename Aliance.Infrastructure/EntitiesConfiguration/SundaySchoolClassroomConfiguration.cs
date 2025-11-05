using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class SundaySchoolClassroomConfiguration : IEntityTypeConfiguration<SundaySchoolClassroom>
{
    public void Configure(EntityTypeBuilder<SundaySchoolClassroom> builder)
    {
        builder.ToTable("SundaySchoolClassroom");

        builder.HasKey(c => c.Id);

        // GUID default
        builder.Property(cm => cm.Guid)
            .HasDefaultValueSql("(UUID())")
            .IsRequired();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Status)
            .IsRequired();

        builder.HasOne(c => c.Church)
            .WithMany(ch => ch.SundaySchoolClassrooms) 
            .HasForeignKey(c => c.ChurchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
