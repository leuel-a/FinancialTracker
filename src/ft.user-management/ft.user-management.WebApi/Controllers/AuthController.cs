using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.user_management.Application.Dtos.Auth;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Features.Auth.Requests.Commands;

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
    /// Authenticates a user and generates access and refresh tokens.
    /// </summary>
    /// <param name="loginUserDto">The user's login credentials.</param>
    /// <returns>A response containing the access and refresh tokens if the authentication is successful,
    /// otherwise a BadRequest response.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        var response = await _mediator.Send(new LoginUserCommand() { userDto = loginUserDto });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(new { response.AccessToken, response.RefreshToken });
    }

    /// <summary>
    /// Refresh the access token using the refresh token. If the refresh token is
    /// valid, a new access token is issued.
    /// </summary>
    /// <param name="refreshTokenDto">The DTO representing the access token and refresh token.</param>
    /// <returns>A response containing the new access token.</returns>
    /// <response code="200">If the valid tokens are provided</response>
    /// <response code="400">If tokens are invalid, the response will be a 400. Bad Request.</response>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshAccessTokenDto refreshTokenDto)
    {
        var response = await _mediator.Send(new RefreshAccessTokenCommand()
            { RefreshAccessTokenDto = refreshTokenDto });

        if (response.Success == false)
            return BadRequest(new
                { response.Message, response.Errors, data = new { AccessToken = response.Resource } });
        return Ok(new { response.Message, response.Errors, data = new { AccessToken = response.Resource } });
    }
}