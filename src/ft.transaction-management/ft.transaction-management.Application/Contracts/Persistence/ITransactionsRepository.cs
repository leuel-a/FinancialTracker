using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    
}