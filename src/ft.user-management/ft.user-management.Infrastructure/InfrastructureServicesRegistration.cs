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
        return services;
    }

    public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var rolesDescription = new Dictionary<string, string>()
        {
            { "Admin", "Has full access to all system functionalities." },
            { "Accountant", "Manages financials and handles accounting tasks." },
            { "Employee", "Standard user with access to basic functionalities." }
        };

        IdentityResult roleResult;

        foreach (var (roleName, description) in rolesDescription)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                roleResult = await roleManager.CreateAsync(new ApplicationRole() { Name = roleName ,Description = description });
            }
        }

        var password = configuration.GetSection("PowerUser:Password").Value!;
        var email = configuration.GetSection("PowerUser:Email").Value!;

        var powerUser = new ApplicationUser
        {
            FirstName = "Leuel",
            LastName = "Gebreseslassie",
            DateCreatedAt = DateTime.Now.ToUniversalTime(),
            LastModifiedAt = DateTime.Now.ToUniversalTime(),
            Email = email
        };

        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            var createPowerUser = await userManager.CreateAsync(powerUser, password);
            if (createPowerUser.Succeeded == true)
            {
                await userManager.AddToRoleAsync(powerUser, "Admin");
            }
        }
    }
}