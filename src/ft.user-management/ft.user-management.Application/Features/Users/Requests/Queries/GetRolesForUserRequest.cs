using MediatR;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Responses;

namespace ft.user_management.Application.Features.Users.Requests.Queries;

public class GetRolesForUserRequest : IRequest<ReadResourceResponse<string>>
{
    public int Id { get; set; } 
}