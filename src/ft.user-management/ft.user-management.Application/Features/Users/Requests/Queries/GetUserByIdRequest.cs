using MediatR;
using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Features.Users.Requests.Queries;

public class GetUserByIdRequest : IRequest<UserDto>
{
    public int Id { get; set; }
}