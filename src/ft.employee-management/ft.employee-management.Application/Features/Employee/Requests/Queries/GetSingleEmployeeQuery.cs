using MediatR;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;

namespace ft.employee_management.Application.Features.Employee.Requests.Queries;

public class GetSingleEmployeeQuery : IRequest<ReadResourceResponse<ReadEmployeeDto>>
{
    public int Id { get; set; }
}