using System;
using System.Threading.Tasks;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface ICategoriesRepository : IGenericRepository<Category>
{
    Task<Category> GetCategoryWithTransactionsAsync(Guid id);
}