using Aliance.Application.Interfaces;
using Aliance.Application.Services;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Repositories;
using Aliance.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Aliance.Crosscutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        // Repositories
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IChurchRepository, ChurchRepository>();
        services.AddScoped<IBaptismRepository, BaptismRepository>();
        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<IMissionCampaignRepository, MissionCampaignRepository>();
        services.AddScoped<ICostCenterRepository, CostCenterRepository>();
        services.AddScoped<IAccountPayableRepository, AccountPayableRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IPatrimonyRepository, PatrimonyRepository>();
        services.AddScoped<IPatrimonyMaintenanceRepository, PatrimonyMaintenanceRepository>();
        services.AddScoped<IPastoralVisitRepository, PastoralVisitRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IAccountReceivableRepository, AccountReceivableRepository>();
        services.AddScoped<ICellMeetingRepository, CellMeetingRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICellMemberRepository, CellMemberRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        // Services
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IChurchService, ChurchService>();
        services.AddScoped<IBaptismService, BaptismService>();
        services.AddScoped<ICellService, CellService>();
        services.AddScoped<ICostCenterService, CostCenterService>();
        services.AddScoped<IAccountPayableService, AccountPayableService>();
        services.AddScoped<IAccountReceivableService, AccountReceivableService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IPatrimonyService, PatrimonyService>();
        services.AddScoped<IPatrimonyMaintenanceService, PatrimonyMaintenanceService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPastoralVisitService, PastoralVisitService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ICellMeetingService, CellMeetingService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICellMemberService, CellMemberService>();
        services.AddScoped<IEventService, EventService>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Token
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
