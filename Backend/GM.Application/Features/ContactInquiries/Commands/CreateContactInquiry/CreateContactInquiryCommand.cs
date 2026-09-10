using MediatR;

namespace GM.Application.Features.ContactInquiries.Commands.CreateContactInquiry;

public record CreateContactInquiryCommand(
    string Name,
    string Email,
    string Subject,
    string Message
) : IRequest; 