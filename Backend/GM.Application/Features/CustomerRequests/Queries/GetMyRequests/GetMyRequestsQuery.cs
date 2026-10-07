using GM.Application.Features.CustomerRequests.DTOs;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetMyRequests;

public record GetMyRequestsQuery(int UserId) : IRequest<IReadOnlyList<CustomerRequestDto>>;
