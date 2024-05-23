using FluentValidation;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Application.Dtos.User.Validators;

public class AddRoleToUserDtoValidator : AbstractValidator<AddRoleToUserDto>
{
    public AddRoleToUserDtoValidator(IUsersService usersService, IRolesService rolesService)
    {
        RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var user = await usersService.GetByIdAsync(id);
                return user != null;
            }).WithMessage("User does not exist");

        RuleFor(p => p.RoleId)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .MustAsync(async (id, cancellationToken) =>
            {
                var role = await rolesService.GetRoleById(id);
                return role != null;
            });
    }
}