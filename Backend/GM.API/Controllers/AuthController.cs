using GM.API.Models.Authentication;
using GM.Application.Features.Authentication.Commands.Login;
using GM.Application.Features.Authentication.Commands.UpdateProfile;
using GM.Application.Features.Authentication.Queries.GetCurrentUser;
using GM.Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var userId = await _mediator.Send(
            new RegisterCommand(
                request.FullName,
                request.Email,
                request.Password),
            cancellationToken);

        return Created(
            $"/api/Auth/{userId}",
            new
            {
                id = userId,
                message = "Account created successfully."
            });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new LoginCommand(
                request.Email,
                request.Password),
            cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(
            new GetCurrentUserQuery(userId),
            cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateCurrentUser(
        [FromBody] UpdateProfileRequest request,
        CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        await _mediator.Send(
            new UpdateProfileCommand(
                userId,
                request.FullName,
                request.Email),
            cancellationToken);

        return Ok(new { message = "Profile updated successfully." });
    }
}