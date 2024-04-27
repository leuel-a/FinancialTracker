using ft.employee_management.Application.Dtos.EmployeeDto;
using MediatR;

namespace ft.employee_management.Application.Features.Employee.Requests.Commands;

public class CreateEmployeeRequest : IRequest<ReadEmployeeDto>
{
    public CreateEmployeeDto EmployeeDto { get; set; }
}