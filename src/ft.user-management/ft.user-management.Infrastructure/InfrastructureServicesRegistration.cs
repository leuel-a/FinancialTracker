using Microsoft.Extensions.DependencyInjection;
using ft.user_management.Infrastructure.Services;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}