using FluentValidation;
using System.Globalization;

namespace ft.employee_management.Application.Dtos.Employee.Validators;

public class BaseEmployeeDtoValidator : AbstractValidator<BaseEmployeeDto>
{
    public BaseEmployeeDtoValidator()
    {
        RuleFor(p => p.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName can not be null.}")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.LastName)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");

        RuleFor(p => p.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .Must(date => DateTime.TryParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var _)).WithMessage("{PropertyName} must be a valid date. Use format yyyy/MM/dd")
            .When(p => p.DateOfBirth != null);

        RuleFor(p => p.HireDate)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Must(date => DateTime.TryParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var _)).WithMessage("{PropertyName} is not a valid date format supported. Use format yyyy/MM/dd");

        RuleFor(p => p.Gender)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Must(g =>
            {
                var gender = char.ToUpper(g[0]) + g[1..];
                return gender is "Male" or "Female";
            });

        RuleFor(p => p.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.Bonus)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");

        RuleFor(p => p.Salary)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");
    }
}