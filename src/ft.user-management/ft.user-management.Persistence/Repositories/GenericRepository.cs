using Microsoft.EntityFrameworkCore;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Persistence.Repositories;

public class GenericRepository<T>(DbContext dbContext) : IGenericRepository<T>
    where T : class
{
    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        // TODO: check if save changes async can throw an exception
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity == null;
    }
}