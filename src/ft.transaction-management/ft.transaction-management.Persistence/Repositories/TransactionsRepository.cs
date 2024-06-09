using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Persistence.Repositories;

public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<decimal> GetExpenseTotalForMonth(int month, int year)
    {
        var expenseTotal = await _context.Transactions
            .Where(t => t.Date.Month == month && t.Date.Year == year && t.Type == "Expense").SumAsync(t => t.Amount);
        return expenseTotal;
    }

    public async Task<Transaction> GetTransactionWithCategory(int transactionId)
    {
        return (await _context.Transactions.Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == transactionId))!;
    }

    public async Task<IReadOnlyList<Transaction>> GetTransactionByCategory(int categoryId, int pageSize,
        int currentPage)
    {
        return await _context.Transactions.Where(t => t.CategoryId == categoryId).Skip((currentPage - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    public async Task<int> CountTransactionsByCategory(int categoryId)
    {
        return await _context.Transactions.CountAsync(t => t.CategoryId == categoryId);
    }

    public async Task<decimal> GetIncomeTotalForMonth(int month, int year)
    {
        var incomeTotal = await _context.Transactions
            .Where(t => t.Date.Month == month && t.Date.Year == year && t.Type == "Income").SumAsync(t => t.Amount);
        return incomeTotal;
    }

    public async Task<IReadOnlyList<Transaction>> GetAllTransactionsWithCategory(IQueryable<Transaction> query,
        int pageSize, int currentPage)
    {
        return await query.Include(t => t.Category).AsNoTracking().Skip((currentPage - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }
}