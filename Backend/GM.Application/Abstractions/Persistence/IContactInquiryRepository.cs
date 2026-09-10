using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface IContactInquiryRepository
{
    Task AddAsync(
        ContactInquiry inquiry,
        CancellationToken cancellationToken = default);
} 