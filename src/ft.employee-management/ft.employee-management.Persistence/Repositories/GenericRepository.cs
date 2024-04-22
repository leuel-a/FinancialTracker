using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ft.employee_management.Application.Contracts.Persistence;

namespace ft.employee_management.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly EmployeeManagementDbContext _context;
    public GenericRepository(EmployeeManagementDbContext context)
    {
        _context = context;
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var entities = await _context.Set<T>().ToListAsync();
        return entities;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}