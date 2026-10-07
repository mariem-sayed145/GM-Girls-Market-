using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly GMDbContext _context;

    public DocumentRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Document document,
        CancellationToken cancellationToken = default)
    {
        await _context.Documents.AddAsync(document, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Document?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Document>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Documents
            .AsNoTracking()
            .Where(d => d.UserId == userId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Document document,
        CancellationToken cancellationToken = default)
    {
        _context.Documents.Remove(document);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
