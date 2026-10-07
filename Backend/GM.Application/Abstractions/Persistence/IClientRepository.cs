using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface IClientRepository
{
    Task AddAsync(Client client, CancellationToken cancellationToken = default);

    Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(Client client, CancellationToken cancellationToken = default);

    Task DeleteAsync(Client client, CancellationToken cancellationToken = default);
}
