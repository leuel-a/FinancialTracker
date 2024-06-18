using MediatR;
using ft.employee_management.Application.Responses;
using ft.employee_management.Application.Contracts.Persistence;
using ft.employee_management.Application.Dtos.Employee.Validators;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

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
        var validationResult = await validator.ValidateAsync(request.DeleteEmployeeDto!, cancellationToken);

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
        response.Message = $"Employee with Id {request.DeleteEmployeeDto.Id} has been successfully deleted.";

        return response;
    }
}