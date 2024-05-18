using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Roles.Requests.Queries;

public class GetRoleByIdRequest : IRequest<ReadResourceResponse<RoleDto>>
{
    public int Id { get; set; }
}