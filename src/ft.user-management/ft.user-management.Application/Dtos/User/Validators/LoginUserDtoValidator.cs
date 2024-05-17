using FluentValidation;

namespace ft.user_management.Application.Dtos.User.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} can not be empty");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");
    } 
}