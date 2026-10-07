namespace GM.Application.Features.Documents.DTOs;

public record DocumentDto(
    int Id,
    int UserId,
    string FileName,
    string ContentType,
    long FileSizeBytes,
    DateTime CreatedAt);
