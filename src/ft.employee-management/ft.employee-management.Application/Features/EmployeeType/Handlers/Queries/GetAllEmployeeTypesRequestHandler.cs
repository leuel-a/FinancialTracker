using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.EmployeeType.Requests.Queries;

namespace ft.employee_management.Application.Features.EmployeeType.Handlers.Queries;

public class GetAllEmployeeTypesRequestHandler : IRequestHandler<GetAllEmployeeTypesRequest, List<EmployeeTypeDto>>
{
    private readonly IEmployeeTypesRepository _employeeTypesRepository;
    private readonly IMapper _mapper;

    public GetAllEmployeeTypesRequestHandler(IEmployeeTypesRepository employeeTypesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _employeeTypesRepository = employeeTypesRepository;
    }

    public async Task<List<EmployeeTypeDto>> Handle(GetAllEmployeeTypesRequest request, CancellationToken cancellationToken)
    {
        var employeeTypes = await _employeeTypesRepository.GetAllAsync();
        return _mapper.Map<List<EmployeeTypeDto>>(employeeTypes);
    }
}