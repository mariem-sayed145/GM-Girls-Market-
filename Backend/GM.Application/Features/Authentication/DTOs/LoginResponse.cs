namespace GM.Application.Features.Authentication.DTOs;

public record LoginResponse(
    int Id,
    string FullName,
    string Email,
    string Token);