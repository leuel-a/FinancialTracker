using MediatR;
using AutoMapper;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.Employee.Validators;
using ft.employee_management.Application.Features.Employee.Requests.Queries;

namespace ft.employee_management.Application.Features.Employee.Handlers.Queries;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ReadResourceResponse<ReadEmployeeDto>>
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;

    public GetAllEmployeesQueryHandler(IMapper mapper, IEmployeesRepository employeesRepository)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
    }

    public async Task<ReadResourceResponse<ReadEmployeeDto>> Handle(GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadEmployeeDto>();
        var validator = new GetAllEmployeesDtoValidator();
        var validationResult = await validator.ValidateAsync(request.GetAllEmployeesDto!);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation error has occured. Please refer the error for more information.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var pageSize = request.GetAllEmployeesDto!.PageSize ?? 10;
        var currentPage = request.GetAllEmployeesDto.CurrentPage ?? 1;

        var employeesQueryable = _employeesRepository.AsQueryable();
        var employees = await _employeesRepository.GetAllAsyncPaginated(employeesQueryable, currentPage, pageSize);

        var totalEmployees = await _employeesRepository.CountAsync(employeesQueryable);
        var totalPages = (int)Math.Ceiling((decimal)totalEmployees / pageSize);
        var nextPage = currentPage < totalPages ? currentPage + 1 : (int?)null;
        var previousPage = currentPage > 1 ? currentPage - 1 : (int?)null;

        response.Succeeded = true;
        response.Message = "Employees have been successfully retrieved.";
        response.CurrentPage = currentPage;
        response.NextPage = nextPage;
        response.PreviousPage = previousPage;
        response.PageSize = pageSize;
        response.TotalRecords = totalEmployees;
        response.Resources = _mapper.Map<IReadOnlyList<ReadEmployeeDto>>(employees);
        return response;
    }
}