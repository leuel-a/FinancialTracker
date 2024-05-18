using MediatR;
using ft.user_management.Application.Responses;

namespace ft.user_management.Application.Features.Roles.Requests.Commands;

public class DeleteRoleCommand : IRequest<BaseResponse>
{
    public int Id { get; set; }
}