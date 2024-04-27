using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ft.employee_management.Persistence.Repositories;
using ft.employee_management.Application.Contracts.Persistence;

namespace ft.employee_management.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var env = configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");

        services.AddDbContext<EmployeeManagementDbContext>(options =>
        {
            // TODO: Populate with data when in Development and only do migrations when in Production
            if (env == "Development")
                options.UseInMemoryDatabase("InMemoryEmployeeManagementDb");
            else
                options.UseNpgsql(configuration.GetConnectionString("EmployeeManagementDb"));
        });

        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        return services;
    }
}