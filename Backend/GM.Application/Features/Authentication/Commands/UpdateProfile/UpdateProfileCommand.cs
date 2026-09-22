using MediatR;

namespace GM.Application.Features.Authentication.Commands.UpdateProfile;

public record UpdateProfileCommand(
    int UserId,
    string FullName,
    string Email) : IRequest;