using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task AddAsync(
        User user,
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);
}