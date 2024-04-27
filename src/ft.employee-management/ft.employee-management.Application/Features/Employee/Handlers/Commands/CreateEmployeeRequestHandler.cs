using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Dtos.EmployeeDto;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

namespace ft.employee_management.Application.Features.Employee.Handlers.Commands;

public class CreateEmployeeRequestHandler : IRequestHandler<CreateEmployeeRequest, ReadEmployeeDto>
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;

    public CreateEmployeeRequestHandler(IEmployeesRepository employeesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
    }
    
    public async Task<ReadEmployeeDto> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Domain.Entities.Employee>(request.EmployeeDto);
        
        employee = await _employeesRepository.AddAsync(employee);
        return _mapper.Map<ReadEmployeeDto>(employee);
    }
}