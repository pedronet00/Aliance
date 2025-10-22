using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class ServiceRoleConfiguration : IEntityTypeConfiguration<ServiceRole>
{
    public void Configure(EntityTypeBuilder<ServiceRole> builder)
    {
        builder.ToTable("ServiceRole");

        builder.HasKey(sr => sr.Id);

        builder.Property(sr => sr.Guid)
            .IsRequired()
            .HasDefaultValueSql("(UUID())")
            .ValueGeneratedOnAdd();

        builder.HasOne(sr => sr.Service)
            .WithMany(s => s.ServiceRoles)
            .HasForeignKey(sr => sr.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sr => sr.Member)
            .WithMany() 
            .HasForeignKey(sr => sr.MemberId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
