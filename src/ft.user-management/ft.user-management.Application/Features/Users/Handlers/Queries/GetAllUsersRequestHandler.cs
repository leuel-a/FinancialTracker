using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, List<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public GetAllUsersRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    
    public async Task<List<UserDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }
}