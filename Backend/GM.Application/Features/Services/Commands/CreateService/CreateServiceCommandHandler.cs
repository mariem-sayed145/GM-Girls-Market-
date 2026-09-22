using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandHandler
    : IRequestHandler<CreateServiceCommand, int>
{
    private readonly IServiceRepository _repository;
    private readonly IFileStorageService _fileStorageService;

    public CreateServiceCommandHandler(
        IServiceRepository repository,
        IFileStorageService fileStorageService)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
    }

    public async Task<int> Handle(
        CreateServiceCommand request,
        CancellationToken cancellationToken)
    {
        var imageUrl = await _fileStorageService.SaveAsync(
            request.ImageStream,
            request.ImageFileName,
            cancellationToken);

        var service = new Service(
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            imageUrl);

        await _repository.AddAsync(service, cancellationToken);

        return service.Id;
    }
}
