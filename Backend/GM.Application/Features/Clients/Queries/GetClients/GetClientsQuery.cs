using GM.Application.Features.Clients.DTOs;
using MediatR;

namespace GM.Application.Features.Clients.Queries.GetClients;

public record GetClientsQuery : IRequest<IReadOnlyList<ClientDto>>;
