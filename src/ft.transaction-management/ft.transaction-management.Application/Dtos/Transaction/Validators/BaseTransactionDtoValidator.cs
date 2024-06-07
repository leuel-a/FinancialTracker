using System;
using FluentValidation;
using ft.transaction_management.Domain.Enums;
using ft.transaction_management.Application.Extensions;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class BaseTransactionDtoValidator<T> : AbstractValidator<T> where T : BaseTransactionDto
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
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .IsValidEnum<T, TransactionTypes>().WithMessage(_ =>
            {
                var enumValues = string.Join(", ", Enum.GetNames(typeof(TransactionTypes)));
                return $"Type is not defined. {enumValues} are valid values.";
            });

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

        RuleFor(p => p.CostPerItem)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}")
            .When(p => p.CostPerItem.HasValue);

        RuleFor(p => p.NumberOfItems)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}")
            .When(p => p.NumberOfItems.HasValue);

        RuleFor(p => p.Status)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .IsValidEnum<T, TransactionStatus>().WithMessage(_ =>
            {
                var enumValues = string.Join(", ", Enum.GetNames(typeof(TransactionStatus)));
                return $"Status is not defined. {enumValues} are valid values.";
            });
    }

    private static bool BeAValidDate(DateTime date)
    {
        return date <= DateTime.UtcNow;
    }
}