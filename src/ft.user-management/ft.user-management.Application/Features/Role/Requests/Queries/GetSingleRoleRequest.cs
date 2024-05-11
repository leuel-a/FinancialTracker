using ft.user_management.Application.Dtos.Role;
using MediatR;

namespace ft.user_management.Application.Features.Role.Requests.Queries;

public class GetSingleRoleRequest : IRequest<RoleDto>
{
    public int Id { get; set; }
}