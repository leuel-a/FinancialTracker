using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Persistence.Contracts;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
}