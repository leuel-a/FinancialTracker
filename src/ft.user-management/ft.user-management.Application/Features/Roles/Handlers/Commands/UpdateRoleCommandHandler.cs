using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.Role.Validators;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Roles.Requests.Commands;

namespace ft.user_management.Application.Features.Roles.Handlers.Commands;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ReadResourceResponse<RoleDto>>
{
    private readonly IRolesService _rolesService;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(IRolesService rolesService, IMapper mapper)
    {
        _mapper = mapper;
        _rolesService = rolesService;
    }

    public async Task<ReadResourceResponse<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        request.RoleDto.Id = request.Id; // TODO: Find a better way to do this with out adding logic to the mediator
        
        var response = new ReadResourceResponse<RoleDto>();
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
        
        var result = await _rolesService.UpdateRoleAsync(role!);
        if (result.Succeeded == false)
        {
            response.Success = false;
            response.Message = "Unable to update role.";
            response.Errors = result.Errors.Select(e => e.Description).ToList();

            return response;
        }
        
        response.Id = request.RoleDto.Id;
        response.Resource = _mapper.Map<RoleDto>(role);
        response.Message = "Role updated successfully.";
        
        return response;
    }
}