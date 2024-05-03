using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public GetUserByIdRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    
    public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.Id);
        return _mapper.Map<UserDto>(user);
    }
}