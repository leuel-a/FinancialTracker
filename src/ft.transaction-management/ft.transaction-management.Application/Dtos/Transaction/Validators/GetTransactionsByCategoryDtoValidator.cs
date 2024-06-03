using FluentValidation;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class GetTransactionsByCategoryDtoValidator : AbstractValidator<GetTransactionsByCategoryDto>
{
    public GetTransactionsByCategoryDtoValidator(ICategoriesRepository categoriesRepository)
    {
        RuleFor(p => p.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MustAsync(async (categoryId, cancellationToken) =>
            {
                var category = await categoriesRepository.GetByIdAsync(categoryId);
                return category != null;
            }).WithMessage("{PropertyName} can not be found. Please check the id.");

        RuleFor(p => p.CurrentPage)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");
    }
}