using GM.Domain.Common;

namespace GM.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    private User()
    {
        // Required by EF Core
    }

    public User(
        string fullName,
        string email,
        string passwordHash)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }
}