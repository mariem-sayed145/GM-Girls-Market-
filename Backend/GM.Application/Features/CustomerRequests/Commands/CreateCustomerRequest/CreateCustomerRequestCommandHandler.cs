using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.CustomerRequests.Commands.CreateCustomerRequest;

public class CreateCustomerRequestCommandHandler : IRequestHandler<CreateCustomerRequestCommand, int>
{
    private readonly ICustomerRequestRepository _repository;
    private readonly IServiceRepository _serviceRepository;

    public CreateCustomerRequestCommandHandler(
        ICustomerRequestRepository repository,
        IServiceRepository serviceRepository)
    {
        _repository = repository;
        _serviceRepository = serviceRepository;
    }

    public async Task<int> Handle(CreateCustomerRequestCommand request, CancellationToken cancellationToken)
    {
        // Ensure service exists
        var service = await _serviceRepository.GetByIdAsync(request.ServiceId, cancellationToken);

        if (service is null)
        {
            throw new KeyNotFoundException("Service not found.");
        }

        var entity = new CustomerRequest(
            request.UserId,
            request.ServiceId,
            request.Description,
            request.PreferredDate);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
