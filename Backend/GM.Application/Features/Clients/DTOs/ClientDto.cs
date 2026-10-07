namespace GM.Application.Features.Clients.DTOs;

public record ClientDto(
    int Id,
    string Name,
    string Email,
    string PhoneNumber,
    string CompanyName,
    DateTime CreatedAt);
