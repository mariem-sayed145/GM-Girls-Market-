using MediatR;

namespace GM.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(int Id)
    : IRequest<ProductDto?>;