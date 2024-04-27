using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Application.Features.EmployeeType.Requests.Queries;
using MediatR;

namespace ft.employee_management.Application.Features.EmployeeType.Handlers.Queries;

public class GetEmployeeTypeRequestHandler : IRequestHandler<GetEmployeeTypeRequest, EmployeeTypeDto>
{
    private readonly IEmployeeTypesRepository _employeeTypesRepository;
    private readonly IMapper _mapper;
    
    public GetEmployeeTypeRequestHandler(IEmployeeTypesRepository employeeTypesRepository, IMapper mapper)
    {
        _employeeTypesRepository = employeeTypesRepository;
        _mapper = mapper;
    }
    
    public async Task<EmployeeTypeDto> Handle(GetEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var employeeType = await _employeeTypesRepository.GetByIdAsync(request.Id);
        return _mapper.Map<EmployeeTypeDto>(employeeType);
    }
}