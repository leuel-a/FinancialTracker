using ft.user_management.Application.Dtos.Users;
using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class UpdateUserCommand : IRequest<UserDto>
{
    public UserDto UserDto { get; set; }
}