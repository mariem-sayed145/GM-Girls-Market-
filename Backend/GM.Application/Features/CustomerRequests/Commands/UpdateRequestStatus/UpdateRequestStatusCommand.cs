using MediatR;

namespace GM.Application.Features.CustomerRequests.Commands.UpdateRequestStatus;

public record UpdateRequestStatusCommand(int Id, int Status) : IRequest;
