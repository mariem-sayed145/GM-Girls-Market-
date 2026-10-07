using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IClientRepository _repository;

    public UpdateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (client is null)
        {
            throw new KeyNotFoundException("Client not found.");
        }

        client.UpdateDetails(
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.CompanyName);

        await _repository.UpdateAsync(client, cancellationToken);
    }
}
