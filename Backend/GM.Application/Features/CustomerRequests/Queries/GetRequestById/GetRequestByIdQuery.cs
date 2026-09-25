using MediatR;

namespace GM.Application.Features.CustomerRequests.Queries.GetRequestById;

public record GetRequestByIdQuery(int Id) : IRequest<CustomerRequestDto?>;
