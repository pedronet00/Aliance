﻿using Aliance.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AccountPayable> AccountPayable{ get; set; }
    public DbSet<Baptism> Baptism{ get; set; }
    public DbSet<Cell> Cell { get; set; }
    public DbSet<Church> Church{ get; set; }
    public DbSet<CostCenter> CostCenter{ get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<MissionCampaign> MissionCampaign { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
