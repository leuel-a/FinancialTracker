using MediatR;
using ft.user_management.Application.Dtos.Role;

namespace ft.user_management.Application.Features.Role.Requests.Commands;

public class CreateRoleCommand : IRequest<RoleDto>
{
    public CreateRoleDto RoleDto { get; set; }
}