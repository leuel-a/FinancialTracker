using System;
using FluentValidation;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class GetTransactionsSummaryDtoValidator : AbstractValidator<GetTransactionsSummaryDto>
{
    public GetTransactionsSummaryDtoValidator()
    {
        RuleFor(p => p.Year)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be less than or equal to the current year.");

        RuleFor(p => p.Month)
            .Cascade(CascadeMode.Stop)
            .Must(month => 0 <= month && month < 12).WithMessage("{PropertyName} must be between 0 and 11.")
            .When(p => p.Month.HasValue);

        RuleFor(p => p.Day)
            .Cascade(CascadeMode.Stop)
            .Must((dto, day) => day >= 1 && day <= DateTime.DaysInMonth(dto.Year, dto.Month ?? 1))
            .WithMessage("{PropertyName} must be a valid day for the given month and year.")
            .When(p => p.Day.HasValue && p.Month.HasValue);
    }
}