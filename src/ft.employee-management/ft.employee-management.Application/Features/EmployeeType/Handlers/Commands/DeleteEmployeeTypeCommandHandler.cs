using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

namespace ft.employee_management.Application.Features.EmployeeType.Handlers.Commands;

public class DeleteEmployeeTypeCommandHandler : IRequestHandler<DeleteEmployeeTypeCommand, Unit>
{
    private readonly IEmployeeTypesRepository _employeeTypesRepository;
    
    public DeleteEmployeeTypeCommandHandler(IEmployeeTypesRepository employeeTypesRepository)
    {
        _employeeTypesRepository = employeeTypesRepository;
    }
    
    public async Task<Unit> Handle(DeleteEmployeeTypeCommand request, CancellationToken cancellationToken)
    {
        var employeeType = await _employeeTypesRepository.GetByIdAsync(request.Id);
        await _employeeTypesRepository.DeleteAsync(employeeType);
        return Unit.Value;
    }
}