using MediatR;
using System.Collections.Generic;
using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Features.Users.Requests.Queries;

public class GetAllUsersRequest : IRequest<List<UserDto>>
{
    
}