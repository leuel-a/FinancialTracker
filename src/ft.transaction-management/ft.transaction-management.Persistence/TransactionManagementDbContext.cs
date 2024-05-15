using System;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Domain.Common;
using Microsoft.EntityFrameworkCore;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Persistence;

public class TransactionManagementDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    public TransactionManagementDbContext(DbContextOptions<TransactionManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionManagementDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        // this is to make it so the DateCreated and LastModifiedDate fields are automatically set
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModifiedDate = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
