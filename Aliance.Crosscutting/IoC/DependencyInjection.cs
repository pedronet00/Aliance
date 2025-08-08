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

        // Services
        services.AddScoped<IDepartmentService, DepartmentService>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
