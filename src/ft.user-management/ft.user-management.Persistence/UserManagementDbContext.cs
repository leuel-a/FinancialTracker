using Microsoft.EntityFrameworkCore;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Persistence;

public class UserManagementDbContext : DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> context) : base(context)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);
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