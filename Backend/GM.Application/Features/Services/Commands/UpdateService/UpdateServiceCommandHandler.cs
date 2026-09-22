using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using MediatR;

namespace GM.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
{
    private readonly IServiceRepository _repository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateServiceCommandHandler(
        IServiceRepository repository,
        IFileStorageService fileStorageService)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
    }

    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (service is null)
        {
            throw new KeyNotFoundException("Service not found.");
        }

        var imageUrl = service.ImageUrl;

        if (request.ImageStream is not null && !string.IsNullOrWhiteSpace(request.ImageFileName))
        {
            imageUrl = await _fileStorageService.SaveAsync(
                request.ImageStream,
                request.ImageFileName,
                cancellationToken);
        }

        service.UpdateDetails(
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            imageUrl);

        await _repository.UpdateAsync(service, cancellationToken);
    }
}
