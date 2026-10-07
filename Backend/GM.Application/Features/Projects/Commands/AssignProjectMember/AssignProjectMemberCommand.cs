using MediatR;

namespace GM.Application.Features.Projects.Commands.AssignProjectMember;

public record AssignProjectMemberCommand(int ProjectId, int UserId) : IRequest;
