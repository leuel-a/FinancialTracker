using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Requests.Queries;

public class GetEmployeeTypeRequest : IRequest<EmployeeTypeDto>
{
    public int Id { get; set; }
}