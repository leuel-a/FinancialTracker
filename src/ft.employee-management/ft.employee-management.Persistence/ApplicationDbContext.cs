using Microsoft.EntityFrameworkCore;
using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContextFactory).Assembly);

        modelBuilder.Entity<Employee>(entity => { entity.Property(e => e.Salary).HasPrecision(20, 5); });
        modelBuilder.Entity<Employee>(entity => { entity.Property(e => e.Bonus).HasPrecision(20, 5); });
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now.ToUniversalTime();
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}