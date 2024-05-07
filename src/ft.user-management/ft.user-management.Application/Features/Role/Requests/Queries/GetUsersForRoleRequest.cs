using MediatR;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Features.Role.Requests.Queries;

public class GetUsersForRoleRequest : IRequest<IReadOnlyList<UserDto>>
{
    public int Id { get; set; }
}