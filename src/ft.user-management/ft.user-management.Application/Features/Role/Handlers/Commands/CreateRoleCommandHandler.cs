using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Role.Requests.Commands;

namespace ft.user_management.Application.Features.Role.Handlers.Commands;

public class CreateRoleCommandHandler: IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRolesRepository _rolesRepository;

    public CreateRoleCommandHandler(IRolesRepository rolesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _rolesRepository = rolesRepository;
    }
    
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Domain.Entities.Role>(request.RoleDto);
        role = await _rolesRepository.AddAsync(role);
        return _mapper.Map<RoleDto>(role);
    }
}