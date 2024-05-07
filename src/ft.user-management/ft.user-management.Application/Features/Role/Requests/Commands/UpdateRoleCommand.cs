using ft.user_management.Application.Dtos.Role;
using MediatR;

namespace ft.user_management.Application.Features.Role.Requests.Commands;

public class UpdateRoleCommand: IRequest<Unit>
{
    public RoleDto RoleDto { get; set; }
}