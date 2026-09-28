using GM.API.Models.Services;
using GM.Application.Features.Services.Commands.CreateService;
using GM.Application.Features.Services.Commands.DeleteService;
using GM.Application.Features.Services.Commands.UpdateService;
using GM.Application.Features.Services.Queries.GetServiceById;
using GM.Application.Features.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create(
        [FromForm] CreateServiceRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Image is null || request.Image.Length == 0)
        {
            return BadRequest("Service image is required.");
        }

        await using var imageStream = request.Image.OpenReadStream();

        var command = new CreateServiceCommand(
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            imageStream,
            request.Image.FileName);

        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var services = await _mediator.Send(new GetServicesQuery(), cancellationToken);

        return Ok(services);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var service = await _mediator.Send(new GetServiceByIdQuery(id), cancellationToken);

        if (service is null)
        {
            return NotFound();
        }

        return Ok(service);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(
        int id,
        [FromForm] UpdateServiceRequest request,
        CancellationToken cancellationToken)
    {
        await using var imageStream = request.Image?.OpenReadStream();

        var command = new UpdateServiceCommand(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            imageStream,
            request.Image?.FileName);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteServiceCommand(id), cancellationToken);

        return NoContent();
    }
}
