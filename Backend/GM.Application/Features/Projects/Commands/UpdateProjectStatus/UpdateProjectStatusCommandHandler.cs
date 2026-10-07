using GM.Application.Abstractions.Persistence;
using GM.Domain.Enums;
using MediatR;

namespace GM.Application.Features.Projects.Commands.UpdateProjectStatus;

public class UpdateProjectStatusCommandHandler : IRequestHandler<UpdateProjectStatusCommand>
{
    private readonly IProjectRepository _repository;

    public UpdateProjectStatusCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        if (!request.IsAdmin)
        {
            var isMember = await _repository.IsMemberAsync(request.Id, request.RequestingUserId, cancellationToken);
            if (!isMember)
            {
                throw new UnauthorizedAccessException("You are not assigned to this project.");
            }
        }

        if (!Enum.IsDefined(typeof(ProjectStatus), request.Status))
        {
            throw new InvalidOperationException("Invalid project status value.");
        }

        project.UpdateStatus((ProjectStatus)request.Status);

        await _repository.UpdateAsync(project, cancellationToken);
    }
}
