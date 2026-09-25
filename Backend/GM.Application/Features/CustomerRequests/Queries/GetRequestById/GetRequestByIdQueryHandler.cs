using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetRequestById;

public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, CustomerRequestDto?>
{
    private readonly ICustomerRequestRepository _repository;

    public GetRequestByIdQueryHandler(ICustomerRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerRequestDto?> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var r = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (r is null)
        {
            return null;
        }

        return new CustomerRequestDto(
            r.Id,
            r.UserId,
            r.ServiceId,
            r.Description,
            r.Status.ToString(),
            r.PreferredDate,
            r.CreatedAt);
    }
}
