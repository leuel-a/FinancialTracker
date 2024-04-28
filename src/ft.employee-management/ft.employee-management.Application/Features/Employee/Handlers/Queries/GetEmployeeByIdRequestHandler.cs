using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.employee_management.Application.Dtos.EmployeeDto;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Features.Employee.Requests.Queries;

namespace ft.employee_management.Application.Features.Employee.Handlers.Queries;

public class GetEmployeeByIdRequestHandler : IRequestHandler<GetEmployeeByIdRequest, EmployeeDto>
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;

    public GetEmployeeByIdRequestHandler(IEmployeesRepository employeesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
    }
    
    public async Task<EmployeeDto> Handle(GetEmployeeByIdRequest request, CancellationToken cancellationToken)
    {
        var employee = await _employeesRepository.GetByIdAsync(request.Id);
        return _mapper.Map<EmployeeDto>(employee);
    }
}