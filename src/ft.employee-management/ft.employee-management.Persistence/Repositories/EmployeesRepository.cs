using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Persistence.Repositories;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    public EmployeesRepository(EmployeeManagementDbContext context) : base(context)
    {
    }
}