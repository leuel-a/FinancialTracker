using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ft.employee_management.Persistence.Repositories;
using ft.employee_management.Application.Contracts.Persistence;

namespace ft.employee_management.Persistence;

public static class PersistenceServicesRegistration
{
   public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<ApplicationDbContext>(options =>
      {
         options.UseSqlServer(configuration.GetConnectionString("EmployeeManagementDB"));
      });

      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IEmployeesRepository, EmployeesRepository>();
      return services;
   }
}