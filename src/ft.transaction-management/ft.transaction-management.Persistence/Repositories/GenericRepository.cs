using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ft.transaction_management.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ft.transaction_management.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TransactionManagementDbContext _dbContext;

    public GenericRepository(TransactionManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetAsync(Guid id)
    {
        return (await _dbContext.Set<T>().FindAsync(id))!;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var exists = await GetAsync(id);
        return exists != null;
    }
}