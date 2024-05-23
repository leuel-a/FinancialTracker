using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.User.Validators;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, BaseResponse>
{
    private readonly IUsersService _usersService;
    private readonly IRolesService _rolesService;

    public AddRoleToUserCommandHandler(IUsersService usersService, IRolesService rolesService)
    {
        _usersService = usersService;
        _rolesService = rolesService;
    }

    public async Task<BaseResponse> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var validator = new AddRoleToUserDtoValidator(_usersService, _rolesService);
        var validationResult = await validator.ValidateAsync(request.AddRoleToUserDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Validation error check error messages for more information.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }

        var role = await _rolesService.GetRoleById(request.AddRoleToUserDto.RoleId);
        var user = await _usersService.GetByIdAsync(request.AddRoleToUserDto.UserId);
        var userRoles = await _usersService.GetRoleAsync(user!);

        if (userRoles.Contains(role!.Name!))
        {
            response.Success = false;
            response.Message = "User already has the role specified.";

            return response;
        }
        
        var result = await _usersService.AddToRoleAsync(user!, role!.Name!);

        if (result.Succeeded == false)
        {
            response.Success = false;
            response.Message = "Something went wrong when adding role to user. Please try again.";
            response.Errors = result.Errors.Select(e => e.Description).ToList();

            return response;
        }
        
        response.Success = true;
        response.Message = "User role successfully updated";

        return response;
    }
}