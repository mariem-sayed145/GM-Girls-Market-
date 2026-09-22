using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface IServiceRepository
{
    Task AddAsync(
        Service service,
        CancellationToken cancellationToken = default);

    Task<Service?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Service>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Service service,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Service service,
        CancellationToken cancellationToken = default);
}
