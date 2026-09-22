using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Authentication.Commands.UpdateProfile;

public class UpdateProfileCommandHandler
    : IRequestHandler<UpdateProfileCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateProfileCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(
        UpdateProfileCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            request.UserId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                "User not found.");
        }

        var email = request.Email
            .Trim()
            .ToLowerInvariant();

        var existingUser =
            await _userRepository.GetByEmailAsync(
                email,
                cancellationToken);

        if (existingUser is not null &&
            existingUser.Id != user.Id)
        {
            throw new InvalidOperationException(
                "An account with this email already exists.");
        }

        user.UpdateProfile(
            request.FullName,
            email);

        await _userRepository.UpdateAsync(
            user,
            cancellationToken);
    }
}