using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Queries;

public class GetUserByIdRequest : IRequest<ReadResourceResponse<ReadUserDto>>
{
    public int Id { get; set; }
}