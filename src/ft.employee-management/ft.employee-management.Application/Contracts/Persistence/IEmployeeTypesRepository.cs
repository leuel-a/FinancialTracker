using System.Threading.Tasks;
using System.Collections.Generic;
using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Application.Contracts.Persistence;

public interface IEmployeeTypesRepository : IGenericRepository<EmployeeType>
{
    Task<EmployeeType> GetEmployeeTypeWithEmployees(int id);
}