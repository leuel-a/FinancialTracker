using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ft.transaction_management.Persistence.Repositories;

public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
{
    private readonly TransactionManagementDbContext _dbContext;

    public TransactionsRepository(TransactionManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Transaction> GetTransactionWithDetails(Guid id)
    {
        var transaction = await _dbContext.Transactions
            .Include(q => q.Category)
            .FirstOrDefaultAsync(q => q.Id == id);
        return transaction;
    }

    public async Task<List<Transaction>> GetTransactionListWithDetails()
    {
        var transactions = await _dbContext.Transactions
            .Include(q => q.Category)
            .ToListAsync();
        return transactions;
    }
}