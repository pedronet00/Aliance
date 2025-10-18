using Microsoft.EntityFrameworkCore;
using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliance.Infrastructure.EntitiesConfiguration
{
    public class ChurchConfiguration : IEntityTypeConfiguration<Church>
    {
        public void Configure(EntityTypeBuilder<Church> builder)
        {
            builder.ToTable("Church");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.CNPJ)
                .IsRequired()
                .HasMaxLength(18);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(50);  

            builder.Property(c => c.State)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Country)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue(true);

        }
    }
}
