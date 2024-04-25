using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Requests;

public class GetEmployeeTypeRequest : IRequest<ReadEmployeeTypeDto>
{
    public int Id { get; set; }
}