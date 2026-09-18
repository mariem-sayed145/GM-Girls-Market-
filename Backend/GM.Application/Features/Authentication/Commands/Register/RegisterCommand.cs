using MediatR;

namespace GM.Application.Features.Authentication.Commands.Register;

public record RegisterCommand(
    string FullName,
    string Email,
    string Password
) : IRequest<int>;