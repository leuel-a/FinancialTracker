using FluentValidation;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Application.Dtos.Category.Validators;

public class UpdateCategoryDtoValidator : BaseCategoryDtoValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator(ICategoriesRepository categoriesRepository)
    {
        RuleFor(p => p.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var category = await categoriesRepository.GetByIdAsync(id);
                return category != null;
            }).WithMessage("Category with id {PropertyValue} does not exist.");
    }
}