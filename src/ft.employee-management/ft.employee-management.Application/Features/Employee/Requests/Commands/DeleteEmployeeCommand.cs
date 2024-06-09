using MediatR;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;

namespace ft.employee_management.Application.Features.Employee.Requests.Commands;

public class DeleteEmployeeCommand : IRequest<BaseResponse>
{
    public DeleteEmployeeDto? DeleteEmployeeDto { get; set; } 
}