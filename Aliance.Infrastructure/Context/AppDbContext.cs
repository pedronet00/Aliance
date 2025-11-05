using Aliance.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AutomaticAccounts> AutomaticAccounts { get; set; }
    public DbSet<AccountPayable> AccountPayable{ get; set; }
    public DbSet<AccountReceivable> AccountReceivable { get; set; }
    public DbSet<Baptism> Baptism{ get; set; }
    public DbSet<Budget> Budget { get; set; }
    public DbSet<Cell> Cell { get; set; }
    public DbSet<CellMeeting> CellMeeting { get; set; }
    public DbSet<CellMember> CellMember { get; set; }
    public DbSet<Church> Church{ get; set; }
    public DbSet<CostCenter> CostCenter{ get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<DepartmentMember> DepartmentMember { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Expense> Expense { get; set; }
    public DbSet<Income> Income { get; set; }
    public DbSet<LeadershipMeetings> LeadershipMeetings { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Mission> Mission { get; set; }
    public DbSet<MissionCampaign> MissionCampaign { get; set; }
    public DbSet<MissionCampaignDonation> MissionCampaignDonation { get; set; }
    public DbSet<MissionMember> MissionMember { get; set; }
    public DbSet<PastoralVisit> PastoralVisit { get; set; }
    public DbSet<Patrimony> Patrimony { get; set; }
    public DbSet<PatrimonyDocument> PatrimonyDocuments { get; set; }
    public DbSet<PatrimonyMaintenanceDocument> PatrimonyMaintenanceDocuments { get; set; }
    public DbSet<PatrimonyMaintenance> PatrimonyMaintenance { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<ServiceRole> ServiceRole { get; set; }
    public DbSet<ServicePresence> ServicePresence { get; set; }
    public DbSet<SundaySchoolClass> SundaySchool { get; set; }
    public DbSet<SundaySchoolClassroom> SundaySchoolClassRoom { get; set; }
    public DbSet<SundaySchoolClass> SundaySchoolClass { get; set; }
    public DbSet<Tithe> Tithe { get; set; }
    public DbSet<WorshipTeam> WorshipTeam { get; set; }
    public DbSet<WorshipTeamMember> WorshipTeamMember { get; set; }
    public DbSet<WorshipTeamRehearsal> WorshipTeamRehearsal { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
