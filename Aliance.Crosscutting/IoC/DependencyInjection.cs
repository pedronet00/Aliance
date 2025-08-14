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

        // Services
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IChurchService, ChurchService>();
        services.AddScoped<IBaptismService, BaptismService>();
        services.AddScoped<ICellService, CellService>();
        services.AddScoped<IMissionCampaignService, MissionCampaignService>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Token
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
