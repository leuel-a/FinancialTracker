using MediatR;
using ft.user_management.Application.Responses;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class DeleteUserCommand : IRequest<BaseResponse>
{
    public int Id { get; set; }
}