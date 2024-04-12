using System;
using System.Threading.Tasks;

using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.DTOs.TransactionDto;

namespace ft.transaction_management.Application.Persistence.Contracts;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    Task<ReadTransactionDto> GetTransactionWithDetails(Guid id);
}