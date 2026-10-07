using MediatR;

namespace GM.Application.Features.Projects.Commands.UpdateProjectStatus;

public record UpdateProjectStatusCommand(
    int Id,
    int Status,
    int RequestingUserId,
    bool IsAdmin) : IRequest;
