using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ft.transaction_management.Persistence.Repositories;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TransactionManagementDB"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();
        return services;
    }
}