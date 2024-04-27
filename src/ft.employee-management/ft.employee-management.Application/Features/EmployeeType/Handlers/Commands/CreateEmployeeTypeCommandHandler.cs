using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

namespace ft.employee_management.Application.Features.EmployeeType.Handlers.Commands;

public class CreateEmployeeTypeCommandHandler : IRequestHandler<CreateEmployeeTypeCommand, EmployeeTypeDto>
{
    private readonly IEmployeeTypesRepository _employeeTypesRepository;
    private readonly IMapper _mapper;
    
    public CreateEmployeeTypeCommandHandler(IEmployeeTypesRepository employeeTypesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _employeeTypesRepository = employeeTypesRepository;
    }
    
    public async Task<EmployeeTypeDto> Handle(CreateEmployeeTypeCommand request, CancellationToken cancellationToken)
    {
        var employeeType = _mapper.Map<Domain.Entities.EmployeeType>(request.CreateEmployeeTypeDto);
        employeeType = await _employeeTypesRepository.AddAsync(employeeType);
        return _mapper.Map<EmployeeTypeDto>(employeeType);
    }
}