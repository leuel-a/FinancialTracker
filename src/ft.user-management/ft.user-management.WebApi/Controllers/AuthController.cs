using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Application.Features.Auth.Requests.Queries;


namespace ft.user_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Authenticates a user with the provided credentials
    /// </summary>
    /// <param name="loginDto">The login data transfer object containing the user's credentials</param>
    /// <returns>Returns the authenticated user and a JWT token</returns>
    /// <response code="200">Returns the authenticated user and a JWT token</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserDto loginDto)
    {
        var response = await _mediator.Send(new AuthenticateUserRequest() { AuthenticateUserDto = loginDto });
        if (response.Success == false)
        {
            return BadRequest(new { response.Message, ValidationError = response.Errors });
        }

        return Ok(new { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken });
    }
}