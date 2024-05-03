using Microsoft.EntityFrameworkCore;
using ft.user_management.Domain.Entities;
using MongoDB.EntityFrameworkCore.Extensions;

namespace ft.user_management.Persistence;

public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);

        // TODO: figure out what this piece of code does
        modelBuilder.Entity<User>().ToCollection("customers");
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModifiedDate = DateTime.Now.ToUniversalTime();

            if (entry.State == EntityState.Added)
                entry.Entity.DateCreatedAt = DateTime.Now.ToUniversalTime();
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}