using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Role.Requests.Queries;

namespace ft.user_management.Application.Features.Role.Handlers.Queries;

public class GetAllRolesRequestHandler: IRequestHandler<GetAllRolesRequest, IReadOnlyList<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolesRepository _rolesRepository;

    public GetAllRolesRequestHandler(IRolesRepository rolesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _rolesRepository = rolesRepository;
    }

    public async Task<IReadOnlyList<RoleDto>> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
    {
        var roles = await _rolesRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<RoleDto>>(roles);
    }
}