using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

public class DeleteEmployeeTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}