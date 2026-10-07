namespace GM.Application.Features.Projects.DTOs;

public record ProjectDto(
    int Id,
    string Name,
    string Description,
    string Status,
    int ClientId,
    string? ClientName,
    IReadOnlyList<ProjectMemberDto> Members,
    DateTime CreatedAt);
