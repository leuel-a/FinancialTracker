using Microsoft.EntityFrameworkCore;
using ft.employee_management.Domain.Entities;
using ft.employee_management.Application.Models;
using ft.employee_management.Application.Contracts.Persistence;

namespace ft.employee_management.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> AsQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id))!;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<int> CountAsync(IQueryable<T> query)
    {
        return await query.CountAsync();
    }

    public async Task<DbOperationResult> AddAsync(T entity)
    {
        var result = new DbOperationResult();
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            // The only way the above operation might be unsuccessful is if it throws an exception
            result.Succeeded = true;
            result.Message = $"{typeof(T)} is successfully added.";
        }
        catch (Exception e)
        {
            result.Succeeded = false;
            result.Message = e.Message;
        }

        return result;
    }

    public async Task<DbOperationResult> UpdateAsync(T entity)
    {
        var result = new DbOperationResult();
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            // the only way the above might be unsuccessful is if it throws  an exception
            result.Succeeded = true;
            result.Message = $"{typeof(T)} is successfully updated.";
        }
        catch (Exception e)
        {
            result.Succeeded = false;
            result.Message = e.Message;
        }

        return result;
    }

    public async Task<DbOperationResult> DeleteAsync(T entity)
    {
        var result = new DbOperationResult();

        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            
            // the only way the above operation might fail is if it throws an exception
            result.Succeeded = true;
            result.Message = $"{typeof(T)} has been successfully deleted.";
        }
        catch (Exception e)
        {
            result.Succeeded = false;
            result.Message = e.Message;
        }

        return result;
    }

    public async Task<IReadOnlyList<T>> GetAllAsyncPaginated(IQueryable<T> queryable, int page, int size)
    {
        return await queryable.Skip((page - 1) * size).AsNoTracking().Take(size).ToListAsync();
    }
}