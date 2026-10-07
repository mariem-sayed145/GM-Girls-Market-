using GM.API.Models.Documents;
using GM.Application.Features.Documents.Commands.DeleteDocument;
using GM.Application.Features.Documents.Commands.UploadDocument;
using GM.Application.Features.Documents.Queries.DownloadDocument;
using GM.Application.Features.Documents.Queries.GetMyDocuments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GM.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(
        [FromForm] UploadDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest("File is required.");
        }

        await using var fileStream = request.File.OpenReadStream();

        var command = new UploadDocumentCommand(
            userId,
            request.File.FileName,
            request.File.ContentType,
            request.File.Length,
            fileStream);

        var documentId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetMyDocuments),
            new { id = documentId },
            new
            {
                id = documentId,
                message = "Document uploaded successfully."
            });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyDocuments(CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        var documents = await _mediator.Send(
            new GetMyDocumentsQuery(userId),
            cancellationToken);

        return Ok(documents);
    }

    [HttpGet("{id:int}/download")]
    public async Task<IActionResult> Download(
        int id,
        CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(
            new DownloadDocumentQuery(id, userId),
            cancellationToken);

        return File(
            result.Stream,
            result.ContentType,
            result.FileName);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idValue, out var userId))
        {
            return Unauthorized();
        }

        await _mediator.Send(
            new DeleteDocumentCommand(id, userId),
            cancellationToken);

        return NoContent();
    }
}
