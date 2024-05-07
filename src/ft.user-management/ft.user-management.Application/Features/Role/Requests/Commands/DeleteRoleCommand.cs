using MediatR;

namespace ft.user_management.Application.Features.Role.Requests.Commands;

public class DeleteRoleCommand: IRequest<Unit>
{
    public int Id { get; set; }
}