using FluentValidation;
using ft.employee_management.Application.Contracts.Persistence;

namespace ft.employee_management.Application.Dtos.Employee.Validators;

public class DeleteEmployeeDtoValidator : AbstractValidator<DeleteEmployeeDto> 
{
    public DeleteEmployeeDtoValidator(IEmployeesRepository employeesRepository)
    {
        RuleFor(p => p.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MustAsync(async (id, token) =>
            {
                var employee = await employeesRepository.GetByIdAsync(id);
                return employee != null;
            }).WithMessage(p => $"Employee with Id {p.Id}");
    } 
}