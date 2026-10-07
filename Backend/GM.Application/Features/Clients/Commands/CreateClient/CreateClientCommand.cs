using MediatR;

namespace GM.Application.Features.Clients.Commands.CreateClient;

public record CreateClientCommand(
    string Name,
    string Email,
    string PhoneNumber,
    string CompanyName) : IRequest<int>;
