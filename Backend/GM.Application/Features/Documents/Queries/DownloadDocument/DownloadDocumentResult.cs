namespace GM.Application.Features.Documents.Queries.DownloadDocument;

public record DownloadDocumentResult(
    Stream Stream,
    string ContentType,
    string FileName);
