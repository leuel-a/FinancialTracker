using FluentValidation;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Application.Dtos.Auth.Validators;

public class RefreshAccessTokenDtoValidator : AbstractValidator<RefreshAccessTokenDto>
{
    public RefreshAccessTokenDtoValidator(ITokenService tokenService)
    {
        RuleFor(x => x.AccessToken)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must((accessToken) => tokenService.ValidToken(accessToken!)).WithMessage("Invalid {PropertyName}");

        RuleFor(x => x.RefreshToken)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must((refreshToken) => tokenService.ValidToken(refreshToken!)).WithMessage("Invalid {PropertyName}");
    }
}