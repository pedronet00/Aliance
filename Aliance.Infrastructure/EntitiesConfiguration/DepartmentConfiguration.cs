using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Department");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(d => d.Church)
            .WithMany(c => c.Departments)
            .HasForeignKey(d => d.ChurchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.CostCenters)
            .WithOne(cc => cc.Department)
            .HasForeignKey(cc => cc.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(d => d.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}
