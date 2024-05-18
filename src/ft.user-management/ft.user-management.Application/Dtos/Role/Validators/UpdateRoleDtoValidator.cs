using FluentValidation;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Application.Dtos.Role.Validators;

public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleDtoValidator(IRolesService rolesService)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} is required cannot be null.")
            .NotEmpty().WithMessage("{PropertyName} can not empty.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var role = await rolesService.GetRoleById(id);
                return role != null;
            });
    }
}