using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.EmployeeType.Requests.Commands;
using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Handlers.Commands;

public class UpdateEmployeeTypeCommandHandler : IRequestHandler<UpdateEmployeeTypeCommand, Unit>
{
    private readonly IEmployeeTypesRepository _employeeTypesRepository;
    private readonly IMapper _mapper;

    public UpdateEmployeeTypeCommandHandler(IEmployeeTypesRepository employeeTypesRepository, IMapper  mapper)
    {
        _mapper = mapper;
        _employeeTypesRepository = employeeTypesRepository;
    }
    
    public async Task<Unit> Handle(UpdateEmployeeTypeCommand request, CancellationToken cancellationToken)
    {
        var employeeType = await _employeeTypesRepository.GetByIdAsync(request.UpdateCreateEmployeeTypeDto.Id);
        
        // Very powerful line of code as it will copy from the existing value to the old value with the AutoMapper
        _mapper.Map(request.UpdateCreateEmployeeTypeDto, employeeType);
        
        await _employeeTypesRepository.UpdateAsync(employeeType);
        return Unit.Value;
    }
}