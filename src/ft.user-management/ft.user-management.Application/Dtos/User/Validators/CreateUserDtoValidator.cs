using FluentValidation;
using System;

namespace ft.user_management.Application.Dtos.User.Validators;

public class CreateUserDtoValidator: AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(p => p.FirstName)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.LastName)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        RuleFor(p => p.DateOfBirth)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("{PropertyName} can not be in the future");

        RuleFor(p => p.Email)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");
    }
}