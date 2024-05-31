using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Persistence.Repositories;

public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
{
    public CategoriesRepository(ApplicationDbContext context) : base(context)
    {
    }
}