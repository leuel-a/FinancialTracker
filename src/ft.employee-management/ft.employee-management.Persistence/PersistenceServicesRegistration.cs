using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ft.employee_management.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EmployeeManagementDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("EmployeeManagementConnectionString"));
        });
        
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        return services;
    }
}