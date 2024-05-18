using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetRolesForUserRequestHandler : IRequestHandler<GetRolesForUserRequest, ReadResourceResponse<string>>
{
    private readonly IUsersService _usersService;

    public GetRolesForUserRequestHandler(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<ReadResourceResponse<string>> Handle(GetRolesForUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<string>();
        var user = await _usersService.GetByIdAsync(request.Id);

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found";

            return response;
        }

        var roles = await _usersService.GetRoleAsync(user);

        response.Success = true;
        response.Message = $"Got roles for user {request.Id}";
        response.Resources = roles;

        return response;
    }
}