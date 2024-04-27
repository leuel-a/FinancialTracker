using MediatR;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;

namespace ft.employee_management.Application.Features.EmployeeType.Requests.Queries;

public class GetEmployeeTypeWithEmployeesRequest : IRequest<EmployeeTypeDto>
{
    public int Id { get; set; }
}