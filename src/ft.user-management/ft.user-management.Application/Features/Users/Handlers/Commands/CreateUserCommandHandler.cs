using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Features.Users.Requests.Commands;
using ft.user_management.Domain.Entities;
using MediatR;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AddAsync(_mapper.Map<User>(request.UserDto));
        return _mapper.Map<UserDto>(user);
    }
}