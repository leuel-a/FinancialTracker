using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Application.Contracts.Persistence;

public interface IEmployeesRepository : IGenericRepository<Employee>
{
}