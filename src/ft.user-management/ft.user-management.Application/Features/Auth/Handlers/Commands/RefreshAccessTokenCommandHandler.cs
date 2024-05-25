using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.Auth.Validators;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Auth.Requests.Commands;

namespace ft.user_management.Application.Features.Auth.Handlers.Commands;

public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, ReadResourceResponse<string>>
{
    private readonly ITokenService _tokenService;
    
    public RefreshAccessTokenCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    public async Task<ReadResourceResponse<string>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<string>();
        var validator = new RefreshAccessTokenDtoValidator(_tokenService);
        var validationResult = await validator.ValidateAsync(request.RefreshAccessTokenDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Unable to issue new access token.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }

        var accessToken = request.RefreshAccessTokenDto!.AccessToken;
        var refreshToken = request.RefreshAccessTokenDto.RefreshToken;

        var newAccessToken = await _tokenService.RefreshAccessToken(accessToken!, refreshToken!);

        if (newAccessToken == null)
        {
            response.Success = false;
            response.Message = "Unable to issue new access token.";
            response.Errors = null;

            return response;
        }

        response.Success = true;
        response.Message = "Successfully issued new access token.";
        response.Resource = newAccessToken;
        
        return response;
    }
}