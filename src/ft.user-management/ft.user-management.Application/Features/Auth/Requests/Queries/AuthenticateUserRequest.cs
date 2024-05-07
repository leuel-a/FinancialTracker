using MediatR;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Features.Auth.Requests.Queries;

public class AuthenticateUserRequest : IRequest<AuthenticateUserCommandResponse>
{
    // TODO: Fix the return value of the request, like return the JWT token string if the user is authenticated
    public AuthenticateUserDto AuthenticateUserDto { get; set; }
}