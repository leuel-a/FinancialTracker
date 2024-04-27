using MediatR;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;

namespace ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

public class CreateEmployeeTypeCommand : IRequest<EmployeeTypeDto>
{
    public CreateEmployeeTypeDto CreateEmployeeTypeDto { get; set; }
}