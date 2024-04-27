using System;
using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Application.Dtos.EmployeeDto;

public class ReadEmployeeDto : BaseEmployeeDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime JoinedDate { get; set; }
    public decimal Salary { get; set; }
    public int EmployeeTypeId { get; set; }
}