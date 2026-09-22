using MediatR;

namespace GM.Application.Features.Services.Commands.CreateService;

public record CreateServiceCommand(
    string Name,
    string Description,
    decimal Price,
    string Category,
    Stream ImageStream,
    string ImageFileName) : IRequest<int>;
