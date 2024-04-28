using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

namespace ft.employee_management.Application.Features.Employee.Handlers.Commands;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeTypeCommand, Unit>
{
    private readonly IEmployeesRepository _employeesRepository;

    public DeleteEmployeeCommandHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }
    
    public async Task<Unit> Handle(DeleteEmployeeTypeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeesRepository.GetByIdAsync(request.Id);
        await _employeesRepository.DeleteAsync(employee);
        return Unit.Value;
    }
}