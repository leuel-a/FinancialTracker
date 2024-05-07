using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ft.user_management.Persistence.Repositories;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserManagementDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        return services;
    }
}