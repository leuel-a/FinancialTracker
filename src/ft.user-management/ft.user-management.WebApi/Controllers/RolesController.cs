using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Features.Roles.Requests.Queries;
using ft.user_management.Application.Features.Roles.Requests.Commands;

namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator meidator)
    {
        _mediator = meidator;
    }

    /// <summary>
    /// Retrieves all roles.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _mediator.Send(new GetAllRolesRequest());
        return response.Success == false ? Ok(new { response.Message, response.Errors }) : Ok(response.Resources);
    }

    /// <summary>
    /// Retrieves a role by ID.
    /// </summary>
    /// <param name="id">The ID of the role to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet("{id:int}", Name = "GetRoleById")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var response = await _mediator.Send(new GetRoleByIdRequest() { Id = id });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(response.Resource);
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    /// <param name="id">The id of the role to be updated</param>
    /// <param name="updateRoleDto">The role data transfer object containing the updated details of the role.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto updateRoleDto)
    {
        var response = await _mediator.Send(new UpdateRoleCommand() { RoleDto = updateRoleDto, Id = id });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });

        return Ok(new { response.Message });
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="createRoleDto">The role data transfer object containing the details of the role to be created.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        var response = await _mediator.Send(new CreateRoleCommand() { RoleDto = createRoleDto });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return CreatedAtRoute("GetRoleById", new { Id = response.Resource!.Id }, response.Resource);
    }

    /// <summary>
    /// Deletes a role by ID.
    /// </summary>
    /// <param name="id">The ID of the role to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var response = await _mediator.Send(new DeleteRoleCommand() { Id = id });

        if (response.Success == false)
            return BadRequest(new { response.Message });

        return Ok(new { response.Message, DeletedResrouce = response.Id });
    }
}