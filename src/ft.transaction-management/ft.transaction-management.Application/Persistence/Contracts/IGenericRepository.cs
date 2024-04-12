using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ft.transaction_management.Application.Persistence.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Guid id);
}