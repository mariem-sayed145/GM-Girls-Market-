using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<int> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var existingUser =
            await _userRepository.GetByEmailAsync(
                email,
                cancellationToken);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "An account with this email already exists.");
        }

        var passwordHash =
            _passwordHasher.Hash(request.Password);

        var user = new User(
            request.FullName.Trim(),
            email,
            passwordHash);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        return user.Id;
    }
}