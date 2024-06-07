using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ft.transaction_management.Domain.Common;
using ft.transaction_management.Application.Models;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
{
    private readonly ApplicationDbContext _context;

    protected GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsyncPaginated(IQueryable<T> query,int currentPage, int pageSize)
    {
        return await query.Skip((currentPage - 1) * pageSize).AsNoTracking().Take(pageSize).ToListAsync();
        // return await _context.Set<T>().AsNoTracking().Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id))!;
    }

    public async Task<DatabaseOperationResult> AddAsync(T entity)
    {
        var operationResult = new DatabaseOperationResult();
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            // The only way the above operation might be unsuccessful is if it throws an exception
            operationResult.Succeeded = true;
            operationResult.Message = $"{typeof(T)} is successfully added.";

            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.Succeeded = false;
            operationResult.Message = e.Message;

            return operationResult;
        }
    }

    public async Task<DatabaseOperationResult> UpdateAsync(T entity)
    {
        var operationResult = new DatabaseOperationResult();

        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // The only way the above might be unsuccessful is if it throws an exception
            operationResult.Succeeded = true;
            operationResult.Message = $"{typeof(T)} is successfully updated.";

            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.Succeeded = false;
            operationResult.Message = e.Message;

            return operationResult;
        }
    }

    public async Task<DatabaseOperationResult> DeleteAsync(T entity)
    {
        var operationResult = new DatabaseOperationResult();

        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            // The only way the above operation might fail is if it throws an exception
            // it is properly handled in the catch statement below
            operationResult.Succeeded = true;
            operationResult.Message = $"{typeof(T)} has successfully been deleted";

            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.Succeeded = false;
            operationResult.Message = e.Message;

            return operationResult;
        }
    }

    public async Task<int> CountAsync(IQueryable<T> query)
    {
        return await query.CountAsync();
    }

    public IQueryable<T> AsQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }
}