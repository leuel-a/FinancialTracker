using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserDto);
        user = await _usersRepository.UpdateAsync(user);
        return _mapper.Map<UserDto>(user);
    }
}