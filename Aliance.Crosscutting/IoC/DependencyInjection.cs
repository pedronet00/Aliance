using Aliance.Application.Interfaces;
using Aliance.Application.Services;
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

        // Services
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IChurchService, ChurchService>();
        services.AddScoped<IBaptismService, BaptismService>();
        services.AddScoped<ICellService, CellService>();
        services.AddScoped<ICostCenterService, CostCenterService>();
        services.AddScoped<IAccountPayableService, AccountPayableService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IUserContextService, UserContextService>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Token
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
