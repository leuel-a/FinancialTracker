using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

public class UpdateEmployeeTypeCommand : IRequest<Unit>
{
    public EmployeeTypeDto UpdateCreateEmployeeTypeDto { get; set; }
}