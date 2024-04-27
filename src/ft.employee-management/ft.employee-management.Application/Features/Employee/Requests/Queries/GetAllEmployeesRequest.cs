using MediatR;
using System.Collections.Generic;
using ft.employee_management.Application.Dtos.EmployeeDto;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;

namespace ft.employee_management.Application.Features.Employee.Requests.Queries;

public class GetAllEmployeesRequest : IRequest<List<ReadEmployeeDto>>
{
}