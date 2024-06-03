using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Models;


namespace ft.transaction_management.Application.Contracts.Persistence;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    Task<int> CountTransactionsByCategory(int categoryId);
    Task<Transaction> GetTransactionWithCategory(int transactionId);
    Task<IReadOnlyList<Transaction>> GetTransactionByCategory(int categoryId, int pageSize, int currentPage);
}