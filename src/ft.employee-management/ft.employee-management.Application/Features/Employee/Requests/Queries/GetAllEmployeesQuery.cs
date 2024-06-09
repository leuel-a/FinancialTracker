using ft.employee_management.Application.Dtos.Employee;
using ft.employee_management.Application.Responses;
using MediatR;

namespace ft.employee_management.Application.Features.Employee.Requests.Queries;

public class GetAllEmployeesQuery : IRequest<ReadResourceResponse<ReadEmployeeDto>>
{
}