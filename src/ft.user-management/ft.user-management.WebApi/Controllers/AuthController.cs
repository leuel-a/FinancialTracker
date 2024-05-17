using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        var response = await _mediator.Send(new LoginUserCommand() { userDto = loginUserDto });

        if (response.Success == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(new { response.AccessToken, response.RefreshToken });
    }
}