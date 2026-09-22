using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Services.Queries.GetServices;

public class GetServicesQueryHandler
    : IRequestHandler<GetServicesQuery, IReadOnlyList<ServiceDto>>
{
    private readonly IServiceRepository _repository;

    public GetServicesQueryHandler(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ServiceDto>> Handle(
        GetServicesQuery request,
        CancellationToken cancellationToken)
    {
        var services = await _repository.GetAllAsync(cancellationToken);

        return services.Select(s => new ServiceDto(
            s.Id,
            s.Name,
            s.Description,
            s.Price,
            s.Category,
            s.ImageUrl)).ToList();
    }
}
