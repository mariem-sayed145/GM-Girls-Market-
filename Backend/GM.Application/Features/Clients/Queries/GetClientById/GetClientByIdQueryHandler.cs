using GM.Application.Abstractions.Persistence;
using GM.Application.Features.Clients.DTOs;
using MediatR;

namespace GM.Application.Features.Clients.Queries.GetClientById;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IClientRepository _repository;

    public GetClientByIdQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (client is null)
        {
            return null;
        }

        return new ClientDto(
            client.Id,
            client.Name,
            client.Email,
            client.PhoneNumber,
            client.CompanyName,
            client.CreatedAt);
    }
}
