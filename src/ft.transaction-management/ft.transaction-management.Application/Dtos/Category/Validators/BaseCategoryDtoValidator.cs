using FluentValidation;

namespace ft.transaction_management.Application.Dtos.Category.Validators;

public class BaseCategoryDtoValidator<T> : AbstractValidator<T> where T: BaseCategoryDto
{
    public BaseCategoryDtoValidator()
    {
        RuleFor(p => p.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty");

        RuleFor(p => p.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");
    }
}