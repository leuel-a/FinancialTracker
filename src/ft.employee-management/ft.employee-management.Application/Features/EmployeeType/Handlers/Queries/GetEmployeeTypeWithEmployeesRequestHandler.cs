using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Application.Features.EmployeeType.Requests.Queries;

namespace ft.employee_management.Application.Features.EmployeeType.Handlers.Queries;

public class GetEmployeeTypeWithEmployeesRequestHandler : IRequestHandler<GetEmployeeTypeWithEmployeesRequest, ReadEmployeeTypeDto>
{
    private readonly IEmployeeTypesRepository _employeeTypesRepository;
    private readonly IMapper _mapper;

    public GetEmployeeTypeWithEmployeesRequestHandler(IEmployeeTypesRepository employeeTypesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _employeeTypesRepository = employeeTypesRepository;
    }
    
    public async Task<ReadEmployeeTypeDto> Handle(GetEmployeeTypeWithEmployeesRequest request, CancellationToken cancellationToken)
    {
        var employeeType = await _employeeTypesRepository.GetEmployeeTypeWithEmployees(request.Id);
        return _mapper.Map<ReadEmployeeTypeDto>(employeeType);
    }
}