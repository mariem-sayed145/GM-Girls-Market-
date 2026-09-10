using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;

namespace GM.Infrastructure.Persistence.Repositories;

public class ContactInquiryRepository : IContactInquiryRepository
{
    private readonly GMDbContext _context;

    public ContactInquiryRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ContactInquiry inquiry,
        CancellationToken cancellationToken = default)
    {
        await _context.ContactInquiries.AddAsync(
            inquiry,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
} 