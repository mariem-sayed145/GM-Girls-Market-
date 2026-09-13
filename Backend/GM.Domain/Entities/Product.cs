using GM.Domain.Common;

namespace GM.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public string Category { get; private set; }

    public string ImageUrl { get; private set; }

    private Product()
    {
        // Required by EF Core
    }

    public void UpdateDetails(
    string name,
    string description,
    decimal price,
    string category,
    string imageUrl)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ImageUrl = imageUrl;
    }

    public Product(
        string name,
        string description,
        decimal price,
        string category,
        string imageUrl)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ImageUrl = imageUrl;

        CreatedAt = DateTime.UtcNow;
    }
}