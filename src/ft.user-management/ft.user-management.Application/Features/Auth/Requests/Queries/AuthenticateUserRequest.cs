using MediatR;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Features.Auth.Requests.Queries;

public class AuthenticateUserRequest : IRequest<AuthenticateUserCommandResponse>
{
    public AuthenticateUserDto AuthenticateUserDto { get; set; }
}