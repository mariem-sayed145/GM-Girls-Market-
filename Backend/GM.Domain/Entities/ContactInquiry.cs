using GM.Domain.Common;

namespace GM.Domain.Entities;

public class ContactInquiry : BaseEntity
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Subject { get; private set; }

    public string Message { get; private set; }

    private ContactInquiry()
    {
        // Required by EF Core
    }

    public ContactInquiry(
        string name,
        string email,
        string subject,
        string message)
    {
        Name = name;
        Email = email;
        Subject = subject;
        Message = message;

        CreatedAt = DateTime.UtcNow;
    }
} 