using GM.Application.Features.ContactInquiries.Commands.CreateContactInquiry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(
        CreateContactInquiryCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            message = "Your inquiry has been submitted successfully."
        });
    }
}