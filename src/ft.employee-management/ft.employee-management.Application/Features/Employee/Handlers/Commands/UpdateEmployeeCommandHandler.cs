using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

namespace ft.employee_management.Application.Features.Employee.Handlers.Commands;

public class UpdateEmployeeCommandHandler: IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IEmployeesRepository employeesRepository, IMapper mapper)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeesRepository.GetByIdAsync(request.EmployeeDto.Id);
        _mapper.Map(request.EmployeeDto, employee);
        
        await _employeesRepository.UpdateAsync(employee);
        return Unit.Value;
    }
}