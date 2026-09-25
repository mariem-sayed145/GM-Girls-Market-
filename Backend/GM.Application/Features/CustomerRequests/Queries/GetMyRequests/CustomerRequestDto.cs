namespace GM.Application.Features.CustomerRequests.Queries.GetMyRequests;

public record CustomerRequestDto(
    int Id,
    int ServiceId,
    string Description,
    string Status,
    DateTime? PreferredDate,
    DateTime CreatedAt);
