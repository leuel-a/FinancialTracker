using ft.user_management.Application.Dtos.Auth;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Auth.Requests.Commands;

public class RefreshAccessTokenCommand: IRequest<ReadResourceResponse<string>>
{
    public RefreshAccessTokenDto? RefreshAccessTokenDto { get; set; }
}