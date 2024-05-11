using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Role.Requests.Queries;

namespace ft.user_management.Application.Features.Role.Handlers.Queries;

public class GetSingleRoleRequestHandler: IRequestHandler<GetSingleRoleRequest, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRolesRepository _rolesRepository;

    public GetSingleRoleRequestHandler(IRolesRepository rolesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _rolesRepository = rolesRepository;
    }
    
    public async Task<RoleDto> Handle(GetSingleRoleRequest request, CancellationToken cancellationToken)
    {
        var role = _rolesRepository.GetByIdAsync(request.Id);
        return _mapper.Map<RoleDto>(role);
    }
}