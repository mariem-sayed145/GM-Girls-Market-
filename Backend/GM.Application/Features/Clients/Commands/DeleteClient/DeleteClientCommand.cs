using MediatR;

namespace GM.Application.Features.Clients.Commands.DeleteClient;

public record DeleteClientCommand(int Id) : IRequest;
