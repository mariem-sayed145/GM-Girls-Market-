using MediatR;

namespace GM.Application.Features.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(int Id) : IRequest;
