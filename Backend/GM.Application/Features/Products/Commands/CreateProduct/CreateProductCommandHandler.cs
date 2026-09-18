using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repository;
    private readonly IFileStorageService _fileStorageService;

    public CreateProductCommandHandler(
        IProductRepository repository,
        IFileStorageService fileStorageService)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
    }

    public async Task<int> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var imageUrl = await _fileStorageService.SaveAsync(
            request.ImageStream,
            request.ImageFileName,
            cancellationToken);

        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.Category,
            imageUrl);

        await _repository.AddAsync(
            product,
            cancellationToken);

        return product.Id;
    }
}

