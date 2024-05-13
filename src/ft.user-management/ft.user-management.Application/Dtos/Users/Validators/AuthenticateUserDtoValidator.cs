using FluentValidation;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Application.Dtos.Users.Validators;

public class AuthenticateUserDtoValidator : AbstractValidator<AuthenticateUserDto>
{
    public AuthenticateUserDtoValidator(IUsersRepository usersRepository)
    {
        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} is required")
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .EmailAddress().WithMessage("{PropertyName} must be an email address");

        RuleFor(p => p.Password)
            .NotNull().WithMessage("{PropertyName} is required")
            .NotEmpty().WithMessage("{PropertyName} can not be empty");
    }
}