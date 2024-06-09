using MediatR;
using ft.employee_management.Application.Dtos;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;

namespace ft.employee_management.Application.Features.Employee.Requests.Queries;

public class GetAllEmployeesQuery : IRequest<ReadResourceResponse<ReadEmployeeDto>>
{
    public GetAllEmployeesDto? GetAllEmployeesDto { get; set; }
}