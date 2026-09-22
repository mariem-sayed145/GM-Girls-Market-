using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly GMDbContext _context;

    public ServiceRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Service service,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<Service>().AddAsync(
            service,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Service?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Service>()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Service>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Service>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Service service,
        CancellationToken cancellationToken = default)
    {
        _context.Set<Service>().Update(service);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Service service,
        CancellationToken cancellationToken = default)
    {
        _context.Set<Service>().Remove(service);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
