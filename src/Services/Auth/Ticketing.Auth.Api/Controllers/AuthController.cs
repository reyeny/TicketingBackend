using Microsoft.AspNetCore.Mvc;
using Ticketing.Auth.Application.Dto;
using Ticketing.Auth.Application.UseCases.Login;
using Ticketing.Auth.Application.UseCases.Register;

namespace Ticketing.Auth.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromServices] RegisterUserHandle handler,
        [FromBody] RegisterRequest request,
        CancellationToken ct)
    {
        var res = await handler.Handle(request, ct);
        return Ok(res);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromServices] LoginUserHandle handler,
        [FromBody] LoginRequest request,
        CancellationToken ct)
    {
        var res = await handler.Handle(request, ct);
        return Ok(res);
    }

    [HttpGet("ping")]
    public IActionResult Ping() => Ok("auth ok");
}