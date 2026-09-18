using GM.Application.Features.Authentication.DTOs;
using MediatR;

namespace GM.Application.Features.Authentication.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginResponse>;