using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class PatrimonyMaintenanceConfiguration : IEntityTypeConfiguration<PatrimonyMaintenance>
{
    public void Configure(EntityTypeBuilder<PatrimonyMaintenance> builder)
    {
        builder.ToTable("PatrimonyMaintenance");

        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.Guid)
            .IsRequired();

        builder.Property(pm => pm.MaintenanceDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(pm => pm.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(pm => pm.Patrimony)
            .WithMany(p => p.Maintenances) 
            .HasForeignKey(pm => pm.PatrimonyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
