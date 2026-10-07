using MediatR;

namespace GM.Application.Features.Projects.Commands.RemoveProjectMember;

public record RemoveProjectMemberCommand(int ProjectId, int UserId) : IRequest;
