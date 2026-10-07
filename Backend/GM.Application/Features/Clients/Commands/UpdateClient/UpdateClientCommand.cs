using MediatR;

namespace GM.Application.Features.Clients.Commands.UpdateClient;

public record UpdateClientCommand(
    int Id,
    string Name,
    string Email,
    string PhoneNumber,
    string CompanyName) : IRequest;
