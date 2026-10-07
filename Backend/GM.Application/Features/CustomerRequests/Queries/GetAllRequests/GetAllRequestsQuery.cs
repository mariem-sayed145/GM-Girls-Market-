using GM.Application.Features.CustomerRequests.DTOs;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetAllRequests;

public record GetAllRequestsQuery : IRequest<IReadOnlyList<CustomerRequestDto>>;
