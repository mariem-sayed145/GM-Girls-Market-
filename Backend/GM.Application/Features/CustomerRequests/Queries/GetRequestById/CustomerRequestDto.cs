namespace GM.Application.Features.CustomerRequests.Queries.GetRequestById;

public record CustomerRequestDto(
    int Id,
    int UserId,
    int ServiceId,
    string Description,
    string Status,
    DateTime? PreferredDate,
    DateTime CreatedAt);
