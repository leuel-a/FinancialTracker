using System;
using FluentValidation;


namespace ft.transaction_management.Application;

public class GetAllCategoriesDtoValidator : AbstractValidator<GetAllCategoriesDto>
{
    public GetAllCategoriesDtoValidator()
    {
        RuleFor(p => p.CurrentPage)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}").When(p => p.CurrentPage.HasValue);

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}").When(p => p.PageSize.HasValue);
    }
}
