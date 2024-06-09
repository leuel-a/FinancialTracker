using FluentValidation;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.Employee.Validators;
using ft.employee_management.Application.Features.Employee.Requests.Commands;
using ft.employee_management.Application.Responses;
using MediatR;

namespace ft.employee_management.Application.Features.Employee.Handlers.Commands;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, BaseResponse>
{
    private readonly IEmployeesRepository _employeesRepository;
    
    public DeleteEmployeeCommandHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }
    
    public async Task<BaseResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var validator = new DeleteEmployeeDtoValidator(_employeesRepository);
        var validationResult = await validator.ValidateAsync(request.DeleteEmployeeDto!);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation errors have occured. Please refer the errors for more information.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var employee = await _employeesRepository.GetByIdAsync(request.DeleteEmployeeDto!.Id);
        var result = await _employeesRepository.DeleteAsync(employee);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
            return response;
        }

        response.Succeeded = true;
        response.Message = "Employee deleted successfully.";

        return response;
    }
}