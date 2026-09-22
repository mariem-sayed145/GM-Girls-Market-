using MediatR;

namespace GM.Application.Features.Authentication.Queries.GetCurrentUser;

public record GetCurrentUserQuery(int UserId) : IRequest<GetCurrentUserResult>;
