using FluentValidation;

namespace ft.user_management.Application.Dtos.Users.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("{PropertyName} is not a valid email address");
    }
}