using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class SundaySchoolClassConfiguration : IEntityTypeConfiguration<SundaySchoolClass>
{
    public void Configure(EntityTypeBuilder<SundaySchoolClass> builder)
    {
        builder.ToTable("SundaySchoolClass");

        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Guid)
            .IsRequired();

        builder.Property(sc => sc.Lesson)
            .IsRequired()
            .HasMaxLength(200);

        // Relacionamento com Teacher (ApplicationUser)
        builder.HasOne(sc => sc.Teacher)
            .WithMany() // se ApplicationUser não tiver coleção de aulas
            .HasForeignKey(sc => sc.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Classroom
        builder.HasOne(sc => sc.SundaySchoolClassroom)
            .WithMany(c => c.Classes) // se SundaySchoolClassroom tiver ICollection<SundaySchoolClass>
            .HasForeignKey(sc => sc.SundaySchoolClassroomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
