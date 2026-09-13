namespace GM.Application.Features.Products.Queries.GetProducts;

public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    string ImageUrl);