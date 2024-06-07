using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Application.Models;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<int> CountAsync(IQueryable<T> query);
    IQueryable<T> AsQueryable();
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<DatabaseOperationResult> AddAsync(T entity);
    Task<DatabaseOperationResult> UpdateAsync(T entity);
    Task<DatabaseOperationResult> DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsyncPaginated(IQueryable<T> query, int page, int size);
}