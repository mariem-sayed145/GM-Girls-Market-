using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Commands.UpdateRequestStatus;

public class UpdateRequestStatusCommandHandler : IRequestHandler<UpdateRequestStatusCommand>
{
    private readonly ICustomerRequestRepository _repository;

    public UpdateRequestStatusCommandHandler(ICustomerRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateRequestStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new KeyNotFoundException("Request not found.");
        }

        if (!Enum.IsDefined(typeof(RequestStatus), request.Status))
        {
            throw new InvalidOperationException("Invalid status.");
        }

        var status = (RequestStatus)request.Status;

        entity.UpdateStatus(status);

        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
