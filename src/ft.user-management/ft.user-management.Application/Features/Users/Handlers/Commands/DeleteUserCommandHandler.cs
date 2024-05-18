using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse>
{
    private readonly IUsersService _usersService;
    
    public DeleteUserCommandHandler(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var user = await _usersService.GetByIdAsync(request.Id);

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found.";
            response.Errors = null;

            return response;
        }

        var result = await _usersService.DeleteAsync(user);

        if (result.Succeeded == false)
        {
            response.Success = true;
            response.Message = "User deletion unsuccessful. Refer the errors for more information.";
            response.Errors = result.Errors.Select(e => e.Description).ToList();

            return response;
        }

        response.Success = true;
        response.Message = "User deletion successful.";
        
        return response;
    }
}