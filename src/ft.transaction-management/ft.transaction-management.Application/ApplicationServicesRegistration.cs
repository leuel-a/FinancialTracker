using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

using MediatR;

namespace ft.transaction_management.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // This is to register all the services in the Application layer
        // to prevent the need to register them one by one in the Presentation Layer
        // This is a good practice to follow as it will make the code more maintainable
        // and easier to manage
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}