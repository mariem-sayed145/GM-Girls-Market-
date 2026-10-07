namespace GM.Domain.Entities;

public class ProjectMember
{
    public int ProjectId { get; private set; }

    public int UserId { get; private set; }

    public DateTime AssignedAt { get; private set; }

    private ProjectMember()
    {
        // Required by EF Core
    }

    public ProjectMember(int projectId, int userId)
    {
        ProjectId = projectId;
        UserId = userId;
        AssignedAt = DateTime.UtcNow;
    }
}
