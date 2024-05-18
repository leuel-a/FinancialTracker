using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Roles.Requests.Commands;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Roles.Handlers.Commands;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, BaseResponse>
{
    private readonly IRolesService _rolesService;

    public DeleteRoleCommandHandler(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }
    
    public async Task<BaseResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var role = await _rolesService.GetRoleById(request.Id);
        
        if (role == null)
        {
            response.Message = "Role not found.";
            response.Success = false;
            return response;
        }

        await _rolesService.DeleteRoleAsync(role);
        
        response.Success = true;
        response.Message = "Role deleted successfully.";
        response.Id = request.Id;
        
        return response;
    }
}