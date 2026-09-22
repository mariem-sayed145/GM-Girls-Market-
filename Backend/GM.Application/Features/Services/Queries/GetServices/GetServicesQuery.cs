using MediatR;

namespace GM.Application.Features.Services.Queries.GetServices;

public record GetServicesQuery : IRequest<IReadOnlyList<ServiceDto>>;
