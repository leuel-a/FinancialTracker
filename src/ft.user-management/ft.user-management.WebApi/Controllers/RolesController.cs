using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Features.Role.Requests.Queries;
using ft.user_management.Application.Features.Role.Requests.Commands;

namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves a list of all roles.
    /// </summary>
    /// <returns>Returns a list of all roles in the database</returns>
    /// <response code="200">If the list of roles is returned successfully</response>
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _mediator.Send(new GetAllRolesRequest());
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a role by its id
    /// </summary>
    /// <param name="id">The id of role</param>
    /// <returns>The role from the database</returns>
    /// <response code="200">The role based on the id from the database</response>
    [HttpGet("{id:int}", Name = "GetRoleById")]
    public async Task<IActionResult> GetSingleRole(int id)
    {
        var response = await _mediator.Send(new GetSingleRoleRequest() { Id = id });
        return Ok(response);
    }

    /// <summary>
    /// Creates a new role
    /// </summary>
    /// <param name="createRoleDto">The role data transfer object containing information about the new role</param>
    /// <returns>The newly created role</returns>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        var response = await _mediator.Send(new CreateRoleCommand() { RoleDto = createRoleDto });
        return CreatedAtRoute("GetRoleById", new { Id = response.Id }, response);
    }
    
    /// <summary>
    /// Deletes a role by its id.
    /// </summary>
    /// <param name="id">The id of the role to be deleted.</param>
    /// <returns>A response indicating the result of the deletion operation.</returns>
    /// <response code="200">If the role is successfully deleted</response>
    /// <response code="404">If the role is not found</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var response = await _mediator.Send(new DeleteRoleCommand() { Id = id});
        return Ok("Role successfully deleted.");
    }

    /// <summary>
    /// Updates a role by its id.
    /// </summary>
    /// <param name="id">The id of the role to be updated.</param>
    /// <param name="roleDto">The data transfer object containing the new information for the role.</param>
    /// <returns>A response indicating the result of the update operation.</returns>
    /// <response code="200">If the role is successfully updated</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto)
    {
        roleDto.Id = id; // This to avoid multiple data from being sent to the command, and its handler
        var response = await _mediator.Send(new UpdateRoleCommand());
        return Ok("Role successfully updated.");
    }
}