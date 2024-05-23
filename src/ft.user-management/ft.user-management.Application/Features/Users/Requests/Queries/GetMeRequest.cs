using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Queries;

public class GetMeRequest : IRequest<ReadResourceResponse<ReadUserDto>>
{
    public string? AccessToken { get; set; }
}