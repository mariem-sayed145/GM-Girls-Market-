using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using MediatR;

namespace GM.Application.Features.Services.Commands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
{
    private readonly IServiceRepository _repository;
    private readonly IFileStorageService _fileStorageService;

    public DeleteServiceCommandHandler(
        IServiceRepository repository,
        IFileStorageService fileStorageService)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
    }

    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (service is null)
        {
            throw new KeyNotFoundException("Service not found.");
        }

        // Optionally delete image file
        await _fileStorageService.DeleteAsync(service.ImageUrl, cancellationToken);

        await _repository.DeleteAsync(service, cancellationToken);
    }
}
