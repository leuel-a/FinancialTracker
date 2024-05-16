using MediatR;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.User;

namespace ft.user_management.Application.Features.Users.Requests.Queries;

public class GetUserByEmailRequest : IRequest<ReadResourceResponse<ReadUserDto>>
{
    public string? Email { get; set; } 
}