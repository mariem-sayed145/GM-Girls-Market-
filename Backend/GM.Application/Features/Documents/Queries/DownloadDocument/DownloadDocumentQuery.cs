using MediatR;

namespace GM.Application.Features.Documents.Queries.DownloadDocument;

public record DownloadDocumentQuery(int Id, int UserId) : IRequest<DownloadDocumentResult>;
