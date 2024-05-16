using FluentValidation;

namespace ft.user_management.Application.Dtos.User.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} can not be required")
            .NotEmpty().WithMessage("{PropertyName} can not be empty");

        RuleFor(p => p.Password)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");
    } 
}