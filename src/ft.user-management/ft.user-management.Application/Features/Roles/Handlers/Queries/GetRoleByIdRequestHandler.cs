using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Roles.Requests.Queries;

namespace ft.user_management.Application.Features.Roles.Handlers.Queries;

public class GetRoleByIdRequestHandler : IRequestHandler<GetRoleByIdRequest, ReadResourceResponse<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolesService _rolesService;

    public GetRoleByIdRequestHandler(IRolesService rolesService, IMapper Mapper)
    {
        _mapper = Mapper;
        _rolesService = rolesService;
    }

    public async Task<ReadResourceResponse<RoleDto>> Handle(GetRoleByIdRequest request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<RoleDto>();
        var role = await _rolesService.GetRoleById(request.Id);

        if (role == null)
        {
            response.Success = false;
            response.Message = "Role not found";

            return response;
        }

        response.Success = true;
        response.Message = "Role found";
        response.Resource = _mapper.Map<RoleDto>(role);

        return response;
    }
}