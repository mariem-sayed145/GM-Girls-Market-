using MediatR;

namespace GM.Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery
    : IRequest<IReadOnlyList<ProductDto>>;