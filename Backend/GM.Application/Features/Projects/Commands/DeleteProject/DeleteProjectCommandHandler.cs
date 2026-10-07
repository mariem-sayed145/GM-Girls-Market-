using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _repository;

    public DeleteProjectCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        await _repository.DeleteAsync(project, cancellationToken);
    }
}
