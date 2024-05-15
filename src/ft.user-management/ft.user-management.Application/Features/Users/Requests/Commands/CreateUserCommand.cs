using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class CreateUserCommand : IRequest<CreatedResourceCommandResponse<ReadUserDto>>
{
    public CreateUserDto UserDto { get; set; }
}