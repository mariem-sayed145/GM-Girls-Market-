using GM.Domain.Entities;

namespace GM.Application.Abstractions.Persistence;

public interface ICustomerRequestRepository
{
    Task AddAsync(
        CustomerRequest request,
        CancellationToken cancellationToken = default);

    Task<CustomerRequest?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CustomerRequest>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CustomerRequest>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        CustomerRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        CustomerRequest request,
        CancellationToken cancellationToken = default);
}
