using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Features.Users.Requests.Queries;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="userDto">The user data transfer object containing the details of the user to be created.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var response = await _mediator.Send(new CreateUserCommand() { UserDto = userDto });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return CreatedAtRoute("GetUserById", new { response.Id }, response.Resource);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="userDto">The user data transfer object containing the updated details of the user.</param>
    /// <param name="id">The ID of the user to be updated.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto, int id)
    {
        var response = await _mediator.Send(new UpdateUserCommand { UserDto = userDto });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(new { response.Message, UserId = response.Id });
    }

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var response = await _mediator.Send(new GetUserByIdRequest() { Id = id });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(response.Resource);
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _mediator.Send(new GetAllUsersRequest());
        return Ok(response.Resources);
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var response = await _mediator.Send(new DeleteUserCommand() { Id = id });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(new { response.Message });
    }
}