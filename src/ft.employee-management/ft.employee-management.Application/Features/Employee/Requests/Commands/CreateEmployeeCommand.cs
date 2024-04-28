using ft.employee_management.Application.Dtos.EmployeeDto;
using MediatR;

namespace ft.employee_management.Application.Features.Employee.Requests.Commands;

public class CreateEmployeeCommand : IRequest<EmployeeDto>
{
    public CreateEmployeeDto EmployeeDto { get; set; }
}