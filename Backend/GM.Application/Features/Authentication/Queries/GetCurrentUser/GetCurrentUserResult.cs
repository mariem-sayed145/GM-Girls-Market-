using GM.Domain.Enums;

namespace GM.Application.Features.Authentication.Queries.GetCurrentUser;

public record GetCurrentUserResult(
    int Id,
    string FullName,
    string Email,
    UserRole Role);
