using System.Collections.Generic;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Requests.Queries;

public class GetAllEmployeeTypesRequest : IRequest<List<ReadEmployeeTypeDto>>
{
}