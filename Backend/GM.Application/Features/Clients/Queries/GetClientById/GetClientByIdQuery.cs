using GM.Application.Features.Clients.DTOs;
using MediatR;

namespace GM.Application.Features.Clients.Queries.GetClientById;

public record GetClientByIdQuery(int Id) : IRequest<ClientDto?>;
