using MediatR;

namespace GM.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    Stream? ImageStream,
    string? ImageFileName
) : IRequest;