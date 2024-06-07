using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace ft.user_management.Infrastructure;

public class UserManagementDbContextFactory : IDesignTimeDbContextFactory<UserManagementDbContext>
{
    public UserManagementDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>();
        var connectionString = configuration.GetConnectionString("IdentityConnection");

        optionsBuilder.UseSqlServer(connectionString);
        return new UserManagementDbContext(optionsBuilder.Options);
    }

    private static IConfiguration BuildConfiguration()
    {
        var builder= new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ft.user-management.WebApi"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: false);
        return builder.Build();
    }
}