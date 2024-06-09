using FluentValidation;

namespace ft.employee_management.Application.Dtos.Employee.Validators;

public class GetAllEmployeesDtoValidator : AbstractValidator<GetAllEmployeesDto>
{
    public GetAllEmployeesDtoValidator()
    {
        RuleFor(p => p.CurrentPage)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.");

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.");
    } 
}