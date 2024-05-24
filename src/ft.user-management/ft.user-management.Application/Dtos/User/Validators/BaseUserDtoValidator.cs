using System;
using FluentValidation;

namespace ft.user_management.Application.Dtos.User.Validators;

public abstract class BaseUserDtoValidator<T> : AbstractValidator<T> where T: BaseUserDto
{
    protected BaseUserDtoValidator()
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
    }
}