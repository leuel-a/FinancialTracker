using MediatR;

namespace ft.employee_management.Application.Features.Employee.Requests.Commands;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}