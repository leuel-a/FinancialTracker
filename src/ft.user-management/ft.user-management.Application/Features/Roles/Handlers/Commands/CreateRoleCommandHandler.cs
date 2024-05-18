using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.Role.Validators;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Roles.Requests.Commands;

namespace ft.user_management.Application.Features.Roles.Handlers.Commands;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ReadResourceResponse<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolesService _rolesService;

    public CreateRoleCommandHandler(IRolesService rolesService, IMapper mapper)
    {
        _mapper = mapper;
        _rolesService = rolesService;
    }

    public async Task<ReadResourceResponse<RoleDto>> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<RoleDto>();
        var validator = new CreateRoleDtoValidator();
        var validationResult = await validator.ValidateAsync(request.RoleDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Validation failed.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }

        var role = _mapper.Map<ApplicationRole>(request.RoleDto);
        var result = await _rolesService.AddRoleAsync(role);

        if (result.Succeeded == false)
        {
            response.Success = false;
            response.Errors = result.Errors.Select(e => e.Description).ToList();
        }

        response.Success = true;
        response.Message = "Role created successfully.";
        response.Resource = _mapper.Map<RoleDto>(role);

        return response;
    }
}