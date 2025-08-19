using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration
{
    public class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            builder.ToTable("CostCenter");

            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cc => cc.DepartmentId)
                .IsRequired();

            builder.Property(cc => cc.ChurchId)
                .IsRequired();

            builder.HasOne(cc => cc.Department)
                .WithMany(d => d.CostCenters)
                .HasForeignKey(cc => cc.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cc => cc.Church)
                .WithMany(c => c.CostCenter)
                .HasForeignKey(cc => cc.ChurchId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
