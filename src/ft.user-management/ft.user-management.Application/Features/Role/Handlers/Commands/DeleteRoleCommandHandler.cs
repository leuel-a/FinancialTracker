using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Role.Requests.Commands;

namespace ft.user_management.Application.Features.Role.Handlers.Commands;

public class DeleteRoleCommandHandler: IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly IRolesRepository _rolesRepository;

    public DeleteRoleCommandHandler(IRolesRepository rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }
    
    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository.GetByIdAsync(request.Id);
        // TODO: Add Validation to check if the role exists, may be use Fluent Validation??
        await _rolesRepository.DeleteAsync(role);
        return Unit.Value;
    }
}