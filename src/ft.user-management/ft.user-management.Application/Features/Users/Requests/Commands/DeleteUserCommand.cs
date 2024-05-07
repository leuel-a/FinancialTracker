using MediatR;

namespace ft.user_management.Application.Features.Users.Requests.Commands;

public class DeleteUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
}