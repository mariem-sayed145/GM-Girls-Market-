using MediatR;

namespace GM.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    string Name,
    string Description,
    int ClientId,
    List<int>? MemberUserIds = null) : IRequest<int>;
