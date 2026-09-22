using GM.Application.Abstractions.Persistence;
using MediatR;

namespace GM.Application.Features.Authentication.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResult>
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetCurrentUserResult> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            request.UserId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return new GetCurrentUserResult(
            user.Id,
            user.FullName,
            user.Email);
    }
}
