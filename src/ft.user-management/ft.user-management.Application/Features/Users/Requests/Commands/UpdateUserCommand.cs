using MediatR;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Responses;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class UpdateUserCommand : IRequest<BaseResponse>
{
    public int Id { get; set; }
    public UpdateUserDto UserDto { get; set; }
}