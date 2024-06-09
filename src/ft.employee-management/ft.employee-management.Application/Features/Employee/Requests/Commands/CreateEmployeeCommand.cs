using MediatR;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;

namespace ft.employee_management.Application.Features.Employee.Requests.Commands;

public class CreateEmployeeCommand : IRequest<CreatedResourceResponse<ReadEmployeeDto>>
{
    public CreateEmployeeDto? CreateEmployeeDto { get; set; } 
}