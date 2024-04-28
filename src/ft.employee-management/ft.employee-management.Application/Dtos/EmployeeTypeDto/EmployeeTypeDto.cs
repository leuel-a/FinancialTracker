using System.Collections.Generic;
using ft.employee_management.Application.Dtos.EmployeeDto;

namespace ft.employee_management.Application.Dtos.EmployeeTypeDto;

public class EmployeeTypeDto : BaseEmployeeTypeDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<EmployeeDto.EmployeeDto>? Employees { get; set; }
}