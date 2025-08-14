using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class BaptismConfiguration : IEntityTypeConfiguration<Baptism>
{
    public void Configure(EntityTypeBuilder<Baptism> builder)
    {
        builder.ToTable("Baptism");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Date)
            .IsRequired()
            .HasColumnType("datetime");

        builder.HasOne(b => b.Pastor)
            .WithMany() 
            .HasForeignKey(b => b.PastorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.User)
            .WithMany() 
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
