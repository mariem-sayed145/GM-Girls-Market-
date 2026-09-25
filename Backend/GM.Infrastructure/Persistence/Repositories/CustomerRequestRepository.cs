using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Repositories;

public class CustomerRequestRepository : ICustomerRequestRepository
{
    private readonly GMDbContext _context;

    public CustomerRequestRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        CustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        await _context.CustomerRequests.AddAsync(
            request,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<CustomerRequest?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.CustomerRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CustomerRequest>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.CustomerRequests
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CustomerRequest>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.CustomerRequests
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        CustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        _context.CustomerRequests.Update(request);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        CustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        _context.CustomerRequests.Remove(request);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
