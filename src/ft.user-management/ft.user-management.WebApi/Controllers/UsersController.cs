using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Features.Users.Requests.Queries;
using ft.user_management.Application.Features.Users.Requests.Commands;

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

    /// <summary>
    /// Deletes a user by their id
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>Returns a message with the deleted user information serialized</returns>
    /// <response code="204">Does not return the deleted user</response> 
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await mediator.Send(new DeleteUserCommand() { Id = id });
        return Ok($"User with {id} has been successfully deleted");
    }

    /// <summary>
    /// Updates an existing user's information based on their id and the provided new user data.
    /// </summary>
    /// <param name="id">The id of the user to be updated. This is part of the URL route.</param>
    /// <param name="userDto">
    ///     The data transfer object containing the new information for the user.
    ///     This is provided in the request body.
    /// </param>
    /// <returns>
    ///     Returns an IActionResult that represents the result of the operation.
    ///     If the operation is successful, it returns a 200 OK status code along with a success message.
    ///     If the operation fails, an appropriate error response is returned.
    /// </returns>
    /// <response code="200">Returns the updated user</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
    {
        userDto.Id = id;
        await mediator.Send(new UpdateUserCommand() { UserDto = userDto });
        return Ok(userDto);
    }
}