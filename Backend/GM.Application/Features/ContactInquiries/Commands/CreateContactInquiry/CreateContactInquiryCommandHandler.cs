using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.ContactInquiries.Commands.CreateContactInquiry;

public class CreateContactInquiryCommandHandler
    : IRequestHandler<CreateContactInquiryCommand>
{
    private readonly IContactInquiryRepository _repository;

    public CreateContactInquiryCommandHandler(
        IContactInquiryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        CreateContactInquiryCommand request,
        CancellationToken cancellationToken)
    {
        var inquiry = new ContactInquiry(
            request.Name,
            request.Email,
            request.Subject,
            request.Message);

        await _repository.AddAsync(
            inquiry,
            cancellationToken);
    }
} 