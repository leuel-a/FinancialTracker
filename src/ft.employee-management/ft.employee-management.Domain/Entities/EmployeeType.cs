using System.Collections.Generic;

namespace ft.employee_management.Domain.Entities;

public class EmployeeType : BaseDomainEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}