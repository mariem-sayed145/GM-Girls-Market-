namespace GM.Application.Features.Services.Queries.GetServiceById;

public record ServiceDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    string ImageUrl);
