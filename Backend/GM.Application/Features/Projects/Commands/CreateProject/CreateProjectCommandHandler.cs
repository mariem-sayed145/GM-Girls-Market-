using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUserRepository _userRepository;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IClientRepository clientRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _clientRepository = clientRepository;
        _userRepository = userRepository;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
        if (client is null)
        {
            throw new KeyNotFoundException("Client not found.");
        }

        var project = new Project(
            request.Name,
            request.Description,
            request.ClientId);

        if (request.MemberUserIds is not null)
        {
            foreach (var userId in request.MemberUserIds.Distinct())
            {
                var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
                if (user is not null)
                {
                    project.AddMember(userId);
                }
            }
        }

        await _projectRepository.AddAsync(project, cancellationToken);

        return project.Id;
    }
}
