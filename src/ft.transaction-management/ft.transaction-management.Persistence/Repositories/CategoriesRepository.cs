using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Persistence.Repositories;

public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
{
    private readonly TransactionManagementDbContext _dbContext;

    public CategoriesRepository(TransactionManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> GetCategoryWithTransactionsAsync(Guid id)
    {
        var category = await _dbContext.Categories
            .Include(q => q.Transactions)
            .FirstOrDefaultAsync(q => q.Id == id);
        return category;
    }
}