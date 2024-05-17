using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Features.Users.Requests.Queries;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var response = await _mediator.Send(new CreateUserCommand() { UserDto = userDto });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return CreatedAtRoute("GetUserById", new { response.Id }, response.Resource);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var response = await _mediator.Send(new GetUserByIdRequest() { Id = id });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(response.Resource);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _mediator.Send(new GetAllUsersRequest());
        return Ok(response.Resources);
    }
}