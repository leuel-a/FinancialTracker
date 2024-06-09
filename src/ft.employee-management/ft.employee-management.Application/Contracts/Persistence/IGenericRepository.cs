using ft.employee_management.Application.Models;
using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> AsQueryable();
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<int> CountAsync(IQueryable<T> query);
    Task<DbOperationResult> AddAsync(T entity);
    Task<DbOperationResult> UpdateAsync(T entity);
    Task<DbOperationResult> DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsyncPaginated(IQueryable<T> queryable, int page, int size);
}