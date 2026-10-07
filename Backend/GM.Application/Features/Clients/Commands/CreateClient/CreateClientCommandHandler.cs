using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly IClientRepository _repository;

    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client(
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.CompanyName);

        await _repository.AddAsync(client, cancellationToken);

        return client.Id;
    }
}
