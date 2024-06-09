using MediatR;
using AutoMapper;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Dtos.Employee;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.Employee.Validators;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

namespace ft.employee_management.Application.Features.Employee.Handlers.Commands;

public class
    CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreatedResourceResponse<ReadEmployeeDto>>
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;

    public CreateEmployeeCommandHandler(IMapper mapper, IEmployeesRepository employeesRepository)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
    }

    public async Task<CreatedResourceResponse<ReadEmployeeDto>> Handle(CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var response = new CreatedResourceResponse<ReadEmployeeDto>();
        var validator = new CreateEmployeeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.CreateEmployeeDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation error has occured please refer the errors for more information.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var employee = _mapper.Map<Domain.Entities.Employee>(request.CreateEmployeeDto);
        var result = await _employeesRepository.AddAsync(employee);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
            return response;
        }

        response.Succeeded = true;
        response.Message = "Employee created successfully.";
        response.Resource = _mapper.Map<ReadEmployeeDto>(employee);
        return response;
    }
}