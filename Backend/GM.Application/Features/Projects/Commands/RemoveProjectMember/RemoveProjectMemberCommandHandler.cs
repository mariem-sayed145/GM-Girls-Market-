using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Projects.Commands.RemoveProjectMember;

public class RemoveProjectMemberCommandHandler : IRequestHandler<RemoveProjectMemberCommand>
{
    private readonly IProjectRepository _projectRepository;

    public RemoveProjectMemberCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(RemoveProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        project.RemoveMember(request.UserId);

        await _projectRepository.UpdateAsync(project, cancellationToken);
    }
}
