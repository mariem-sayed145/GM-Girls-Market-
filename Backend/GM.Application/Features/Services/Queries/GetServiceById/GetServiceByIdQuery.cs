using MediatR;

namespace GM.Application.Features.Services.Queries.GetServiceById;

public record GetServiceByIdQuery(int Id) : IRequest<ServiceDto?>;
