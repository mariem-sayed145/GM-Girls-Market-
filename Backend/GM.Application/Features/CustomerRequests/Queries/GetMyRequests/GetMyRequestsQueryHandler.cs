using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetMyRequests;

public class GetMyRequestsQueryHandler : IRequestHandler<GetMyRequestsQuery, IReadOnlyList<CustomerRequestDto>>
{
    private readonly ICustomerRequestRepository _repository;

    public GetMyRequestsQueryHandler(ICustomerRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CustomerRequestDto>> Handle(GetMyRequestsQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);

        return list.Select(r => new CustomerRequestDto(
            r.Id,
            r.ServiceId,
            r.Description,
            r.Status.ToString(),
            r.PreferredDate,
            r.CreatedAt)).ToList();
    }
}
