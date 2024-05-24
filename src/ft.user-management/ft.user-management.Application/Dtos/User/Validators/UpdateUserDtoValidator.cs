using FluentValidation;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Application.Dtos.User.Validators;

public class UpdateUserDtoValidator : BaseUserDtoValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator(IUsersService usersService)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .MustAsync(async (id, _) =>
            {
                var user = await usersService.GetByIdAsync(id);
                return user != null;
            }).WithMessage("User with the given id does not exist.");
    }
}