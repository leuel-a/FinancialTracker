using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Features.Users.Requests.Commands;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ISender mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves a user by their id
    /// </summary>
    /// <param name="id">The ID of the product</param>
    /// <returns>Returns the product with the specified ID</returns>
    /// <response code="200">Returns the product with the specified ID</response>
    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await mediator.Send(new GetUserByIdRequest() { Id = id });
        return Ok(user);
    }

    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>Returns a list of all registered users</returns>
    /// <response code="200">If the list of users is returned successfully</response>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await mediator.Send(new GetAllUsersRequest());
        return Ok(users);
    }
    
    /// <summary>
    /// Creates a new user with the provided user data.
    /// </summary>
    /// <param name="userDto">The user data transfer object containing information for the new user</param>
    /// <returns>Returns the created user and a URI to the newly created user in the Location header</returns>
    /// <response code="201">Returns the newly created user</response>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var user = await mediator.Send(new CreateUserCommand() { UserDto = userDto });
        return CreatedAtRoute("GetUserById", new { Id = user.Id }, user);
    }
}