using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ft.transaction_management.Application.Persistence.Contracts;

public interface IGenericRepository
{
    Task<T> GetAsync<T>(Guid id);
    Task<List<T>> GetAllAsync<T>();
    Task<T> AddAsync<T>(T entity);
    Task UpdateAsync<T>(T entity);
    Task DeleteAsync<T>(Guid id);
    Task ExistsAsync<T>(Guid id);
}