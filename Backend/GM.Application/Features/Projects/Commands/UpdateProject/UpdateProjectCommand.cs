using MediatR;

namespace GM.Application.Features.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    int Id,
    string Name,
    string Description,
    int ClientId) : IRequest;
