using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.user_management.Application.Features.Role.Requests.Queries;

namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Retrieves a list of all roles.
    /// </summary>
    /// <returns>Returns a list of all roles in the system</returns>
    /// <response code="200">If the list of roles is returned successfully</response>
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _mediator.Send(new GetAllRolesRequest());
        return Ok(roles);
    }
}