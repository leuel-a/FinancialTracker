using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ft.user_management.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ft.user_management.Infrastructure;

public class UserManagementDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
        {
            entry.Entity.LastModifiedAt = DateTime.Now.ToUniversalTime();

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreatedAt = DateTime.Now.ToUniversalTime();
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}