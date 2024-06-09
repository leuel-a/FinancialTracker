using MediatR;
using AutoMapper;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.Employee.Requests.Queries;

namespace ft.employee_management.Application.Features.Employee.Handlers.Queries;

public class GetSingleEmployeeQueryHandler : IRequestHandler<GetSingleEmployeeQuery, ReadResourceResponse<ReadEmployeeDto>>
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper; 
    
    public GetSingleEmployeeQueryHandler(IMapper mapper, IEmployeesRepository employeesRepository)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
    }
    
    public async Task<ReadResourceResponse<ReadEmployeeDto>> Handle(GetSingleEmployeeQuery request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadEmployeeDto>();

        var employee = await _employeesRepository.GetByIdAsync(request.Id);

        if (employee == null)
        {
            response.Succeeded = false;
            response.Message = $"Employee with ID {request.Id}";

            return response;
        }

        response.Succeeded = true;
        response.Message = "Employee found";
        response.Resource = _mapper.Map<ReadEmployeeDto>(employee);
        return response;
    }
}