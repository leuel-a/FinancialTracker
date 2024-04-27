using ft.employee_management.Domain.Entities;
using System.Threading.Tasks;

namespace ft.employee_management.Application.Contracts.Persistence;

public interface IEmployeesRepository : IGenericRepository<Employee>
{
    Task<Employee> GetEmployeeDetails(int id);
}   