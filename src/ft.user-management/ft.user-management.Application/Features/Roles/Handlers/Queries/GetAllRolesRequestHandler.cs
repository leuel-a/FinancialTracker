using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Roles.Requests.Queries;

namespace ft.user_management.Application.Features.Roles.Handlers.Queries;

public class GetAllRolesRequestHandler : IRequestHandler<GetAllRolesRequest, ReadResourceResponse<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolesService _rolesService;

    public GetAllRolesRequestHandler(IRolesService rolesService, IMapper mapper)
    {
        _mapper = mapper;
        _rolesService = rolesService;
    }

    public async Task<ReadResourceResponse<RoleDto>> Handle(GetAllRolesRequest request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<RoleDto>();
        var roles = await _rolesService.GetAllRoles();

        response.Success = true;
        response.Message = "Roles retrieved successfully.";
        response.Resources = _mapper.Map<List<RoleDto>>(roles);

        return response;
    }
}