using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler
    : IRequestHandler<GetServiceByIdQuery, ServiceDto?>
{
    private readonly IServiceRepository _repository;

    public GetServiceByIdQueryHandler(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceDto?> Handle(
        GetServiceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (service is null)
        {
            return null;
        }

        return new ServiceDto(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.Category,
            service.ImageUrl);
    }
}
