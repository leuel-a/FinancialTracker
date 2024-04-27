using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ft.employee_management.Domain.Entities;
using ft.employee_management.Application.Contracts.Persistence;

namespace ft.employee_management.Persistence.Repositories;

public class EmployeeTypesRepository : GenericRepository<EmployeeType>, IEmployeeTypesRepository
{
    private readonly EmployeeManagementDbContext _context;

    public EmployeeTypesRepository(EmployeeManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<EmployeeType> GetEmployeeTypeWithEmployees(int id)
    {
        return await _context.EmployeeTypes.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
    }
}