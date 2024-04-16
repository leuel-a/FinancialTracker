using System;
using System.Threading.Tasks;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    Task<ReadTransactionDto> GetTransactionWithDetails(Guid id);
}