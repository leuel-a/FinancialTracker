using FluentValidation;

namespace ft.user_management.Application.Dtos.Role.Validators;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.");
    }
}