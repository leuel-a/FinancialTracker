using ft.employee_management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ft.employee_management.Persistence;

public class EmployeeManagementDbContext : DbContext
{
    public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModified = DateTime.Now.ToUniversalTime();

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now.ToUniversalTime();
            }
        }
        
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}