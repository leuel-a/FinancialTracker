using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.EmployeeDto;
using ft.employee_management.Application.Features.Employee.Requests.Queries;

namespace ft.employee_management.Application.Features.Employee.Handlers.Queries;

public class GetAllEmployeesRequestHandler(IEmployeesRepository employeesRepository, IMapper mapper)
    : IRequestHandler<GetAllEmployeesRequest, List<EmployeeDto>>
{
    public async Task<List<EmployeeDto>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
    {
        var employees = await employeesRepository.GetAllAsync();
        return mapper.Map<List<EmployeeDto>>(employees);
    }
}