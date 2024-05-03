using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ft.user_management.Persistence.Repositories;
using ft.user_management.Infrastructure.Data.Settings;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        MongoDbSettings? mongoDbSettings)
    {
        services.AddDbContext<UserManagementDbContext>(opts =>
        {
            opts.UseInMemoryDatabase("InMemoryFtUserManagementDatabase");
            // opts.UseMongoDB(mongoDbSettings!.ConnectionString, mongoDbSettings.DatabaseName);
        });

        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}