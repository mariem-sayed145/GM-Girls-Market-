using GM.API.Models.CustomerRequests;
using GM.Application.Features.CustomerRequests.Commands.CreateCustomerRequest;
using GM.Application.Features.CustomerRequests.Commands.UpdateRequestStatus;
using GM.Application.Features.CustomerRequests.Queries.GetMyRequests;
using GM.Application.Features.CustomerRequests.Queries.GetRequestById;
using GM.Application.Features.CustomerRequests.Queries.GetMyRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerRequestRequest request,
        CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        var command = new CreateCustomerRequestCommand(
            userId,
            request.ServiceId,
            request.Description,
            request.PreferredDate);

        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyRequests(CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        var list = await _mediator.Send(new GetMyRequestsQuery(userId), cancellationToken);

        return Ok(list);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        // Management endpoint: returns all customer requests
        var list = await _mediator.Send(new GM.Application.Features.CustomerRequests.Queries.GetAllRequests.GetAllRequestsQuery(), cancellationToken);

        return Ok(list);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = await _mediator.Send(new GetRequestByIdQuery(id), cancellationToken);

        if (request is null)
        {
            return NotFound();
        }

        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        if (request.UserId != userId)
        {
            // If the current user is not the owner, deny access. Management UI should use the admin endpoints.
            return Forbid();
        }

        return Ok(request);
    }

    [Authorize]
    [HttpGet("admin/{id:int}")]
    public async Task<IActionResult> GetByIdAdmin(int id, CancellationToken cancellationToken)
    {
        var request = await _mediator.Send(new GetRequestByIdQuery(id), cancellationToken);

        if (request is null)
        {
            return NotFound();
        }

        return Ok(request);
    }

    [Authorize]
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] int status, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateRequestStatusCommand(id, status), cancellationToken);

        return NoContent();
    }
}
