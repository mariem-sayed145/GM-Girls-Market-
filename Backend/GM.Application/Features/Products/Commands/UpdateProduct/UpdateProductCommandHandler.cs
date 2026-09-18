using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using MediatR;

namespace GM.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProductCommandHandler(
        IProductRepository repository,
        IFileStorageService fileStorageService)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
    }

    public async Task Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
        {
            throw new KeyNotFoundException(
                $"Product with ID '{request.Id}' was not found.");
        }

        var imageUrl = product.ImageUrl;

        if (request.ImageStream is not null &&
            !string.IsNullOrWhiteSpace(request.ImageFileName))
        {
            imageUrl = await _fileStorageService.SaveAsync(
                request.ImageStream,
                request.ImageFileName,
                cancellationToken);

            await _fileStorageService.DeleteAsync(
                product.ImageUrl,
                cancellationToken);
        }

        product.UpdateDetails(
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            imageUrl);

        await _repository.UpdateAsync(
            product,
            cancellationToken);
    }
}
