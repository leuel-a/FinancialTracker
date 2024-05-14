using ft.user_management.Application.Dtos.User;
using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class CreateUserCommand : IRequest<Unit>
{
    public CreateUserDto UserDto { get; set; }
}