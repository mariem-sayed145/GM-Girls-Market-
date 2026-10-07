using GM.Domain.Common;

namespace GM.Domain.Entities;

public class Client : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PhoneNumber { get; private set; } = null!;

    public string CompanyName { get; private set; } = null!;

    private Client()
    {
        // Required by EF Core
    }

    public Client(
        string name,
        string email,
        string phoneNumber,
        string companyName)
    {
        Name = name.Trim();
        Email = email.Trim().ToLowerInvariant();
        PhoneNumber = phoneNumber.Trim();
        CompanyName = companyName.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(
        string name,
        string email,
        string phoneNumber,
        string companyName)
    {
        Name = name.Trim();
        Email = email.Trim().ToLowerInvariant();
        PhoneNumber = phoneNumber.Trim();
        CompanyName = companyName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}
