using MediatR;

namespace GM.Application.Features.Services.Commands.DeleteService;

public record DeleteServiceCommand(int Id) : IRequest;
