using MediatR;

namespace GM.Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery(
    string? Search,
    string? Category,
    decimal? MinPrice,
    decimal? MaxPrice) : IRequest<IReadOnlyList<ProductDto>>;
