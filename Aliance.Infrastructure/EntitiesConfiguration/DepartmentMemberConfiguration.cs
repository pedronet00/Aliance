using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class DepartmentMemberConfiguration : IEntityTypeConfiguration<DepartmentMember>
{
    public void Configure(EntityTypeBuilder<DepartmentMember> builder)
    {
        builder.ToTable("DepartmentMember");

        builder.HasKey(dm => dm.Id);

        builder.HasOne(dm => dm.Department)
            .WithMany(d => d.Members)
            .HasForeignKey(dm => dm.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dm => dm.User)
            .WithMany()
            .HasForeignKey(dm => dm.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(dm => dm.JoinedDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(dm => dm.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}
