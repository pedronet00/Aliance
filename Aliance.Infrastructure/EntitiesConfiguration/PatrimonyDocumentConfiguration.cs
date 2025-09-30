using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class PatrimonyDocumentConfiguration : IEntityTypeConfiguration<PatrimonyDocument>
{
    public void Configure(EntityTypeBuilder<PatrimonyDocument> builder)
    {
        builder.ToTable("PatrimonyDocuments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Guid)
               .IsRequired();

        builder.Property(d => d.FileName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(d => d.ContentType)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.FilePath)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(d => d.UploadedAt)
               .IsRequired();

        // Relacionamento
        builder.HasOne(d => d.Patrimony)
               .WithMany(p => p.Documents)
               .HasForeignKey(d => d.PatrimonyId)
               .OnDelete(DeleteBehavior.Cascade);

        // Índice único no Guid
        builder.HasIndex(d => d.Guid).IsUnique();
    }
}
