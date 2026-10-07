using GM.Application.Abstractions.Persistence;
using GM.Application.Features.Clients.DTOs;
using MediatR;

namespace GM.Application.Features.Clients.Queries.GetClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IReadOnlyList<ClientDto>>
{
    private readonly IClientRepository _repository;

    public GetClientsQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _repository.GetAllAsync(cancellationToken);

        return clients
            .Select(c => new ClientDto(
                c.Id,
                c.Name,
                c.Email,
                c.PhoneNumber,
                c.CompanyName,
                c.CreatedAt))
            .ToList();
    }
}
