using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Role.Requests.Queries;

namespace ft.user_management.Application.Features.Role.Handlers.Queries;

public class GetUsersForRoleRequestHandler : IRequestHandler<GetUsersForRoleRequest, IReadOnlyList<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolesRepository _rolesRepository;
    private readonly IUsersRepository _usersRepository;

    public GetUsersForRoleRequestHandler(IMapper mapper, IUsersRepository usersRepository,
        IRolesRepository rolesRepository)
    {
        _mapper = mapper;
        _rolesRepository = rolesRepository;
        _usersRepository = usersRepository;
    }

    public async Task<IReadOnlyList<UserDto>> Handle(GetUsersForRoleRequest request, CancellationToken cancellationToken)
    {
        var users = await _rolesRepository.GetUsersForRole(request.Id);
        return _mapper.Map<IReadOnlyList<UserDto>>(users);
    }
}