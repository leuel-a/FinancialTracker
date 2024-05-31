using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Persistence.Repositories;

public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
{
    public TransactionsRepository(ApplicationDbContext context) : base(context)
    {
    }
}