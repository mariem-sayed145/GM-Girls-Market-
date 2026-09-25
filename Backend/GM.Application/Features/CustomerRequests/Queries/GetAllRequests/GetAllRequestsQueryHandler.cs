using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetAllRequests;

public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, IReadOnlyList<CustomerRequestDto>>
{
    private readonly ICustomerRequestRepository _repository;

    public GetAllRequestsQueryHandler(ICustomerRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CustomerRequestDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetAllAsync(cancellationToken);

        return list.Select(r => new CustomerRequestDto(
            r.Id,
            r.UserId,
            r.ServiceId,
            r.Description,
            r.Status.ToString(),
            r.PreferredDate,
            r.CreatedAt)).ToList();
    }
}
