using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetAllRequests;

public record GetAllRequestsQuery : IRequest<IReadOnlyList<CustomerRequestDto>>;
