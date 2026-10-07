using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface IProjectRepository
{
    Task AddAsync(Project project, CancellationToken cancellationToken = default);

    Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> GetAssignedToUserAsync(int userId, CancellationToken cancellationToken = default);

    Task UpdateAsync(Project project, CancellationToken cancellationToken = default);

    Task DeleteAsync(Project project, CancellationToken cancellationToken = default);

    Task<bool> IsMemberAsync(int projectId, int userId, CancellationToken cancellationToken = default);
}
