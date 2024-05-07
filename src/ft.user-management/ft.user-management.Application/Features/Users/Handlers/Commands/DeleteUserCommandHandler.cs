using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;
    
    public DeleteUserCommandHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.Id);
        await _usersRepository.DeleteAsync(user!);
        return Unit.Value;
    }
}