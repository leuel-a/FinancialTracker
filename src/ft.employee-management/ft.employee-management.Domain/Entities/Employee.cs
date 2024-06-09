using ft.employee_management.Domain.Enums;

namespace ft.employee_management.Domain.Entities;

public class Employee : BaseDomainEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Bonus { get; set; }
    public decimal Salary { get; set; }
    public EmployeeType EmployeeType { get; set; }
}
