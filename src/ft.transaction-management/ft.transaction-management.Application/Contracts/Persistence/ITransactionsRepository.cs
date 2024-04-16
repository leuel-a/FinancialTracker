using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    Task<Transaction> GetTransactionWithDetails(Guid id);
    Task<List<Transaction>> GetTransactionListWithDetails();
}