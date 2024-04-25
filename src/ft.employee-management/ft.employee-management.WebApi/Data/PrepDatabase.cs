using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ft.employee_management.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ft.employee_management.WebApi.Data;

public static class PrepDatabase
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProd)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
    }

    private static void SeedData(EmployeeManagementDbContext context, bool isProd)
    {
        if (isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not run migrations: {e.Message}");
            }
        }

        if (!context.Employees.Any())
        {
            
        }
    }
}