using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Dtos.Users.Validators;
using ft.user_management.Application.Features.Users.Requests.Commands;
using ft.user_management.Application.Responses;
using ft.user_management.Domain.Entities;
using MediatR;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedResourceCommandResponse<UserDto>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<CreatedResourceCommandResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new CreatedResourceCommandResponse<UserDto>();
        var validator = new CreateUserDtoValidator();
        var validatorResult = await validator.ValidateAsync(request.UserDto, cancellationToken);
        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Validation failed, please make sure you have the correct data";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

            return response;
        }
        
        // TODO: hash password of the incoming token
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
        request.UserDto.Password = passwordHash;
        
        var user = await _usersRepository.AddAsync(_mapper.Map<User>(request.UserDto));

        response.Success = true;
        response.Message = "User has been registered successfully";
        response.Id = user.Id;
        response.Resource = _mapper.Map<UserDto>(user);
        return response;
    }
}