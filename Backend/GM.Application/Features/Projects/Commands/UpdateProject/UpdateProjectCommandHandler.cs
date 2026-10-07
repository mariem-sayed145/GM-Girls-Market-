using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IClientRepository _clientRepository;

    public UpdateProjectCommandHandler(
        IProjectRepository projectRepository,
        IClientRepository clientRepository)
    {
        _projectRepository = projectRepository;
        _clientRepository = clientRepository;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
        if (client is null)
        {
            throw new KeyNotFoundException("Client not found.");
        }

        project.UpdateDetails(
            request.Name,
            request.Description,
            request.ClientId);

        await _projectRepository.UpdateAsync(project, cancellationToken);
    }
}
