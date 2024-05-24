using FluentValidation;
using System;

namespace ft.user_management.Application.Dtos.User.Validators;

public class CreateUserDtoValidator : BaseUserDtoValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("{PropertyName} must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("{PropertyName} must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("{PropertyName} must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("{PropertyName} must contain at least one special character.");

        RuleFor(p => p.Username)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Matches("[a-zA-Z0-9]").WithMessage("{PropertyName} can only contain letters or digits");
    }
}