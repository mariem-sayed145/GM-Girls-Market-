using MediatR;

namespace GM.Application.Features.Services.Commands.UpdateService;

public record UpdateServiceCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    Stream? ImageStream,
    string? ImageFileName) : IRequest;
