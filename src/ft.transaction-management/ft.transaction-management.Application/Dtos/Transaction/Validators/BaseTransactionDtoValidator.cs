using System;
using FluentValidation;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class BaseTransactionDtoValidator<T>: AbstractValidator<T> where T : BaseTransactionDto
{
    protected BaseTransactionDtoValidator()
    {
        RuleFor(p => p.Amount)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MaximumLength(300).WithMessage("{PropertyName} must be less than 300 characters.");

        RuleFor(p => p.Date)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Must(BeAValidDate).WithMessage("{PropertyName} is not a valid date.");
    }
    
    private static bool BeAValidDate(DateTime date)
    {
        return date <= DateTime.UtcNow;
    }
}