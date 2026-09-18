using GM.API.Models.Authentication;
using GM.Application.Features.Authentication.Commands.Login;
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
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        var fullName = User.FindFirstValue(
            ClaimTypes.Name);

        var email = User.FindFirstValue(
            ClaimTypes.Email);

        return Ok(new
        {
            id = userId,
            fullName,
            email
        });
    }
}