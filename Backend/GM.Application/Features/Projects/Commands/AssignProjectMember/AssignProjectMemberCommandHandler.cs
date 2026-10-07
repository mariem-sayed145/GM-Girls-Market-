using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Projects.Commands.AssignProjectMember;

public class AssignProjectMemberCommandHandler : IRequestHandler<AssignProjectMemberCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public AssignProjectMemberCommandHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(AssignProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        project.AddMember(request.UserId);

        await _projectRepository.UpdateAsync(project, cancellationToken);
    }
}
