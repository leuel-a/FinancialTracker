using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class CreateUserCommand : IRequest<CreatedResourceCommandResponse<UserDto>>
{
    public CreateUserDto UserDto { get; set; }
}