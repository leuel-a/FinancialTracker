using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Exception = System.Exception;
using Microsoft.AspNetCore.Identity; 
using Microsoft.Extensions.Configuration;
using ft.user_management.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ft.user_management.Infrastructure;

public static class IdentitySetupAndTearDown
{
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

        foreach (var (roleName, description) in rolesDescription)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (roleExists)
            {
                Console.WriteLine($"Role: {roleName} exists. No need to createRole");
                continue;
            }

            var roleResult = await roleManager.CreateAsync(new ApplicationRole()
                { Name = roleName, Description = description });

            if (roleResult.Succeeded != false) continue;
            
            await DeleteAllUsersAndRoles(serviceProvider);
            throw new Exception($"Error creating role: {roleName}");
        }

        var password = configuration.GetSection("PowerUser:Password").Value!;
        var email = configuration.GetSection("PowerUser:Email").Value!;

        var powerUser = new ApplicationUser
        {
            FirstName = "Leuel",
            LastName = "Gebreseslassie",
            UserName = "leuel",
            DateCreatedAt = DateTime.Now.ToUniversalTime(),
            LastModifiedAt = DateTime.Now.ToUniversalTime(),
            Email = email
        };

        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            var result = await userManager.CreateAsync(powerUser, password);
            if (result.Succeeded == false)
            {
                await DeleteAllUsersAndRoles(serviceProvider);
                throw new Exception($"Error creating power user: {powerUser.Email}");
            }

            var roleAddedToPowerUser = await userManager.AddToRoleAsync(powerUser, "Admin");
            if (roleAddedToPowerUser.Succeeded == false)
            {
                await DeleteAllUsersAndRoles(serviceProvider);
                throw new Exception($"Error adding admin role to power user: {powerUser.Email}");
            }
        }

        Console.WriteLine("Power user already exists");
    }

    public static async Task DeleteAllUsersAndRoles(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        var users = userManager.Users.ToList();
        var roles = roleManager.Roles.ToList();

        foreach (var user in users)
        {
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded == false)
                throw new Exception($"Failed to delete user: {user.Email}");
        }

        foreach (var role in roles)
        {
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded == false)
                throw new Exception($"Failed to delete role: {role.Name}");
        }

        Console.WriteLine("All users and roles have been deleted.");
    }
}