using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface IDocumentRepository
{
    Task AddAsync(Document document, CancellationToken cancellationToken = default);

    Task<Document?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Document>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task DeleteAsync(Document document, CancellationToken cancellationToken = default);
}
