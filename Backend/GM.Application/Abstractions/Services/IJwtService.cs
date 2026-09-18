using GM.Domain.Entities;

namespace GM.Application.Abstractions.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}