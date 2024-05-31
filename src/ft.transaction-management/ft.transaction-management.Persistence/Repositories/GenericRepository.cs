using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Models;
using ft.transaction_management.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ft.transaction_management.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsyncPaginated(int page, int size)
    {
        return await _context.Set<T>().AsNoTracking().Skip((page - 1) * size).Take(size).ToListAsync();
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
            var result = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            if (result.State == EntityState.Added)
            {
                operationResult.Succeeded = true;
                operationResult.Message = "Entity added successfully.";
            }
            else
            {
                operationResult.Succeeded = false;
                operationResult.Message = "Entity not added.";

                return operationResult;
            }
            
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
            // TODO: There must be a better way to do this
            var originalState = _context.Entry(entity).State;
            
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            var currentState = _context.Entry(entity).State;
            if (originalState != currentState)
            {
                operationResult.Succeeded = true;
                operationResult.Message = "Entity updated successfully.";
            }
            else
            {
                operationResult.Succeeded = false;
                operationResult.Message = "Entity not updated.";
            }

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
            var result = _context.Remove(entity);
            await _context.SaveChangesAsync();

            if (result.State == EntityState.Deleted)
            {
                operationResult.Succeeded = false;
                operationResult.Message = "Entity deleted successfully.";
            }
            else
            {
                operationResult.Succeeded = false;
                operationResult.Message = "Entity not deleted.";
            }

            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.Succeeded = false;
            operationResult.Message = e.Message;

            return operationResult;
        }
    }
}