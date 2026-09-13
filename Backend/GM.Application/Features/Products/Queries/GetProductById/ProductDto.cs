namespace GM.Application.Features.Products.Queries.GetProductById;

public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    string ImageUrl);