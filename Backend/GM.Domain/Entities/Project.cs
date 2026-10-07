using GM.Domain.Common;
using GM.Domain.Enums;

namespace GM.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public ProjectStatus Status { get; private set; }

    public int ClientId { get; private set; }

    public Client? Client { get; private set; }

    private readonly List<ProjectMember> _members = new();
    public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();

    private Project()
    {
        // Required by EF Core
    }

    public Project(
        string name,
        string description,
        int clientId,
        ProjectStatus status = ProjectStatus.NotStarted)
    {
        Name = name.Trim();
        Description = description.Trim();
        ClientId = clientId;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string description, int clientId)
    {
        Name = name.Trim();
        Description = description.Trim();
        ClientId = clientId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(ProjectStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMember(int userId)
    {
        if (!_members.Any(m => m.UserId == userId))
        {
            _members.Add(new ProjectMember(Id, userId));
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveMember(int userId)
    {
        var existing = _members.FirstOrDefault(m => m.UserId == userId);
        if (existing is not null)
        {
            _members.Remove(existing);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
