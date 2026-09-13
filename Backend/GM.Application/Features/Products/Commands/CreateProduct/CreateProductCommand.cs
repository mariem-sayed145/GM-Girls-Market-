using MediatR;

namespace GM.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Category,
    string ImageUrl
) : IRequest<int>;