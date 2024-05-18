using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Roles.Requests.Commands;

public class CreateRoleCommand : IRequest<ReadResourceResponse<RoleDto>>
{
    public CreateRoleDto RoleDto { get; set; } 
}