using MediatR;
using AutoMapper;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.Employee.Validators;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

namespace ft.employee_management.Application.Features.Employee.Handlers.Commands;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, BaseResponse>
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;

    public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeesRepository employeesRepository)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
    }
    
    public async Task<BaseResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var validator = new UpdateEmployeeDtoValidator(_employeesRepository);
        var validationResult = await validator.ValidateAsync(request.UpdateEmployeeDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation errors have occured. Please refer the errors for more information.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var employee = _mapper.Map<Domain.Entities.Employee>(request.UpdateEmployeeDto);
        var result = await _employeesRepository.UpdateAsync(employee);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
            return response;
        }
        
        response.Succeeded = true;
        response.Message = "Employee updated successfully.";
        return response;
    }
}