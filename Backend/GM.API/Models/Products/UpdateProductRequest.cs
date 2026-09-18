using Microsoft.AspNetCore.Http;

namespace GM.API.Models.Products;

public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Category { get; set; } = string.Empty;

    public IFormFile? Image { get; set; }
}

