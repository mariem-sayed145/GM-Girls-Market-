using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _repository;

    public DeleteClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (client is null)
        {
            throw new KeyNotFoundException("Client not found.");
        }

        await _repository.DeleteAsync(client, cancellationToken);
    }
}
