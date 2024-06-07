using System;
using FluentValidation;
using ft.transaction_management.Domain.Enums;
using ft.transaction_management.Application.Extensions;
using System.Globalization;

namespace ft.transaction_management.Application;

public class GetAllTransactionsDtoValidator : AbstractValidator<GetAllTransactionsDto>
{
    public GetAllTransactionsDtoValidator()
    {
        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyValue} must be greater than or equal to {ComparisonValue}")
            .When(p => p.PageSize.HasValue);

        RuleFor(p => p.CurrentPage)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyValue} must be greater than or equal to {ComparisonValue}");

        RuleFor(p => p.StartDate)
            .Cascade(CascadeMode.Stop)
            .Must(date =>
            {
                if (!DateTime.TryParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _))
                    return false;
                return true;
            }).WithMessage("{PropertyName} must be a valid date. Use format yyyy/MM/dd.")
            .When(p => p.StartDate != null);

        RuleFor(p => p.EndDate)
            .Cascade(CascadeMode.Stop)
            .Must(date =>
            {
                if (!DateTime.TryParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    return false;
                return true;
            }).WithMessage("{PropertyName} must be a valid date. Use format yyyy/MM/dd.")
            .When(p => p.EndDate != null);

        RuleFor(p => p).Must(p =>
        {
            if (p.StartDate != null && p.EndDate != null)
            {
                if (!DateTime.TryParseExact(p.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate))
                    return false;
                if (!DateTime.TryParseExact(p.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
                    return false;
                return parsedStartDate <= parsedEndDate;
            }
            return true;
        }).WithMessage("Start date must be less than or equal to the end date.");

        RuleFor(p => p.Type)
            .IsValidEnum<GetAllTransactionsDto, TransactionTypes>()
            .WithMessage(p =>
            {
                var enumValues = Enum.GetNames(typeof(TransactionTypes));
                return $"Type {p.Type} is not a valid query paramters. Allowed types are {string.Join(", ", enumValues)} .";
            })
            .When(p => p.Type != null);

        RuleFor(p => p.Status)
            .IsValidEnum<GetAllTransactionsDto, TransactionStatus>()
            .WithMessage(p =>
            {
                var enumValues = Enum.GetNames(typeof(TransactionStatus));
                return $"Status {p.Status} is not a valid query parameter. Allowed status are {string.Join(", ", enumValues)}";
            })
            .When(p => p.Status != null);
    }
}
