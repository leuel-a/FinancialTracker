using MediatR;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Role;

namespace ft.user_management.Application.Features.Role.Requests.Queries;

public class GetAllRolesRequest: IRequest<IReadOnlyList<RoleDto>>
{
}