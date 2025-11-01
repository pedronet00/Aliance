using Aliance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.EntitiesConfiguration;

public class TitheConfiguration : IEntityTypeConfiguration<Tithe>
{
    public void Configure(EntityTypeBuilder<Tithe> builder)
    {

        builder.ToTable("Tithe");

        builder.HasKey(t => t.Id);


        builder.OwnsOne(ap => ap.Amount, money =>
        {
            money.Ignore(m => m.Notifications);
            money.Property(m => m.Value)
                 .HasColumnName("Amount")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();
        });
            builder.Property(t => t.Date)
            .IsRequired();

    }
}
