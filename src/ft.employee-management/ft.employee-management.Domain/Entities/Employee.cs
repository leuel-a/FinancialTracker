using ft.employee_management.Domain.Enums;

namespace ft.employee_management.Domain.Entities;

public class Employee : BaseDomainEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime JoinedDate { get; set; }
    public decimal Salary { get; set; }
    public EmployeeType Type { get; set; }
}