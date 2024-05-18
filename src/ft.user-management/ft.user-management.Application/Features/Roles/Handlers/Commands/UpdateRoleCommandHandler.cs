using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.Role.Validators;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Roles.Requests.Commands;

namespace ft.user_management.Application.Features.Roles.Handlers.Commands;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, BaseResponse>
{
    private readonly IRolesService _rolesService;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(IRolesService rolesService, IMapper mapper)
    {
        _mapper = mapper;
        _rolesService = rolesService;
    }

    public async Task<BaseResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var validator = new UpdateRoleDtoValidator(_rolesService);
        var validationResult = await validator.ValidateAsync(request.RoleDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Message = "Unable to update role.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var role = await _rolesService.GetRoleById(request.RoleDto.Id);
        role = _mapper.Map(request.RoleDto, role);
        await _rolesService.UpdateRoleAsync(role!);
        
        response.Id = request.RoleDto.Id;
        response.Message = "Role updated successfully.";
        
        return response;
    }
}