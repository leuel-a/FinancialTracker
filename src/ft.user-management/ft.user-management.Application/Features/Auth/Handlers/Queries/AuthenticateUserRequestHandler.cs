using MediatR;
using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Contracts.Persistence;
using ft.user_management.Application.Dtos.Users.Validators;
using ft.user_management.Application.Features.Auth.Requests.Queries;

namespace ft.user_management.Application.Features.Auth.Handlers.Queries;

public class AuthenticateUserRequestHandler : IRequestHandler<AuthenticateUserRequest, AuthenticateUserCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUsersRepository _usersRepository;

    public AuthenticateUserRequestHandler(IUsersRepository usersRepository, IMapper mapper, ITokenService tokenService)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _usersRepository = usersRepository;
    }
    
    public async Task<AuthenticateUserCommandResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
        var response = new AuthenticateUserCommandResponse();
        var validator = new AuthenticateUserDtoValidator(_usersRepository);
        var validatorResult = await validator.ValidateAsync(request.AuthenticateUserDto, cancellationToken);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Login unsuccessful, validation failed";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

            return response;
        }
        
        var user = await _usersRepository.GetUserByEmail(request.AuthenticateUserDto.Email!);
        var token = _tokenService.GenerateToken(user!);

        response.Success = true;
        response.Message = "Login successful, token generated";
        response.Token = token;
        
        return response;
    }
}