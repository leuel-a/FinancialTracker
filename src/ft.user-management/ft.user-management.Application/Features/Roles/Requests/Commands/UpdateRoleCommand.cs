using MediatR;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;

namespace ft.user_management.Application.Features.Roles.Requests.Commands;

public class UpdateRoleCommand : IRequest<BaseResponse>
{
    public UpdateRoleDto? RoleDto { get; set; } 
}