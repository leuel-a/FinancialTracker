using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Role.Requests.Commands;

namespace ft.user_management.Application.Features.Role.Handlers.Commands;

public class UpdateRoleCommandHandler: IRequestHandler<UpdateRoleCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IRolesRepository _rolesRepository;
    
    public UpdateRoleCommandHandler(IRolesRepository rolesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _rolesRepository = rolesRepository;
    }
    
    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository.GetByIdAsync(request.RoleDto.Id);
        _mapper.Map(request.RoleDto, role);
        await _rolesRepository.UpdateAsync(role);
        return Unit.Value;
    }
}