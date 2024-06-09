using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Models;

namespace ft.employee_management.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T: class
{
    public IQueryable<T> AsQueryable()
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync(IQueryable<T> query)
    {
        throw new NotImplementedException();
    }

    public async Task<DbOperationResult> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<DbOperationResult> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<DbOperationResult> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> GetAllAsyncPaginated(IQueryable<T> queryable, int page, int size)
    {
        throw new NotImplementedException();
    }
}