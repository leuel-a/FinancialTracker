using FluentValidation;
using System;

namespace ft.user_management.Application.Dtos.User.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.DateOfBirth)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Must(dateOfBirth => DateTime.TryParseExact(dateOfBirth, "yyyy/MM/dd", null,
                System.Globalization.DateTimeStyles.None, out _))
            .WithMessage("Date: {PropertyValue} is not a valid date. Supported format is yyyy/MM/dd.")
            .Must(dateOfBirth => DateTime.Parse(dateOfBirth!) <= DateTime.Today)
            .WithMessage("{PropertyName} can not be in the future");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .EmailAddress().WithMessage("Email is not a valid email address.");

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