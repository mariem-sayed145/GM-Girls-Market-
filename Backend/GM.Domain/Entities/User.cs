using GM.Domain.Common;
using GM.Domain.Enums;

namespace GM.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public UserRole Role { get; private set; }

    private User()
    {
        
    }

    public User(
        string fullName,
        string email,
        string passwordHash,
        UserRole role = UserRole.Customer)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(
        string fullName,
        string email)
    {
        FullName = fullName.Trim();
        Email = email.Trim().ToLowerInvariant();
        UpdatedAt = DateTime.UtcNow;
    }
}

