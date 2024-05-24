using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ft.user_management.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ft.user_management.Infrastructure.Services;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

        services.AddIdentity<ApplicationUser, ApplicationRole>(
                options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<UserManagementDbContext>().AddDefaultTokenProviders();

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}