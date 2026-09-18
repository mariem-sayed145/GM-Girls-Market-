using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DeleteProductCommand request,
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

        await _repository.DeleteAsync(
            product,
            cancellationToken);
    }
}