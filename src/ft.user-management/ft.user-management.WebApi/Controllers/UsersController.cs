using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Features.Users.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ft.user_management.WebApi.Controllers;

public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var response = await _mediator.Send(new CreateUserCommand() { UserDto = userDto });
        
    }
}