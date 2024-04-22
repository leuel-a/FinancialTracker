using MediatR;
using System.Collections.Generic;
using ft.employee_management.Application.Dtos.EmployeeDto;

namespace ft.employee_management.Application.Features.Employee.Requests.Queries;

public class GetAllEmployeesRequest : IRequest<List<ReadEmployeeDto>>
{
}