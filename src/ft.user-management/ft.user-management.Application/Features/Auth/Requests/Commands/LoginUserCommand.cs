using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Auth.Requests.Commands;

public class LoginUserCommand : IRequest<LoggedInUserResponse>
{
    public LoginUserDto userDto { get; set; }
}