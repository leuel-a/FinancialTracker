using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Application.Models;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface IGenericRepository <T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsyncPaginated(int page, int size);
    Task<T> GetByIdAsync(int id);
    Task<DatabaseOperationResult> AddAsync(T entity);
    Task<DatabaseOperationResult> UpdateAsync(T entity);
    Task<DatabaseOperationResult> DeleteAsync(T entity);
    Task<int> CountAsync();
}