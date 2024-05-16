using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Dtos.User.Validators;
using ft.user_management.Application.Features.Auth.Requests.Commands;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Auth.Handlers.Commands;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoggedInUserResponse>
{
    private readonly IUsersService _usersService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IUsersService usersService, ITokenService tokenService, IMapper mapper)
    {
        _mapper = mapper;
        _usersService = usersService;
        _tokenService = tokenService;
    }

    public async Task<LoggedInUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = new LoggedInUserResponse();
        var validator = new LoginUserDtoValidator();
        var validatorResult = await validator.ValidateAsync(request.userDto, cancellationToken);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Validation failed, please fix the errors.";
            response.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var user = await _usersService.GetUserByEmailAsync(request.userDto.Email);
        if (user == null)
        {
            response.Success = false;
            response.Message = "Email or password is not correct";
            return response;
        }

        var passwordMatch = await _usersService.VerifyPasswordAsync(user, request.userDto.Password);

        if (!passwordMatch)
        {
            response.Success = false;
            response.Message = "Email or password is not correct";
            return response;
        }

        var accessToken = await _tokenService.GenerateToken(user);
        var refreshToken = await _tokenService.GenerateToken(user, "RefreshToken");

        response.Success = true;
        response.Message = "Login Successful";
        response.AccessToken = accessToken;
        response.RefreshToken = refreshToken;

        return response;
    }
}