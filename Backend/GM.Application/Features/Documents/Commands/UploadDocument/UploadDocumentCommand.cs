using MediatR;

namespace GM.Application.Features.Documents.Commands.UploadDocument;

public record UploadDocumentCommand(
    int UserId,
    string FileName,
    string ContentType,
    long FileSizeBytes,
    Stream FileStream) : IRequest<int>;
