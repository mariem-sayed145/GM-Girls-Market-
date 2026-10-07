using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using MediatR;

namespace GM.Application.Features.Documents.Queries.DownloadDocument;

public class DownloadDocumentQueryHandler : IRequestHandler<DownloadDocumentQuery, DownloadDocumentResult>
{
    private readonly IDocumentRepository _repository;
    private readonly IDocumentStorageService _storageService;

    public DownloadDocumentQueryHandler(
        IDocumentRepository repository,
        IDocumentStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    public async Task<DownloadDocumentResult> Handle(
        DownloadDocumentQuery request,
        CancellationToken cancellationToken)
    {
        var document = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (document is null)
        {
            throw new KeyNotFoundException("Document not found.");
        }

        if (document.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You do not have permission to access this document.");
        }

        var stream = await _storageService.GetDocumentStreamAsync(document.StoragePath, cancellationToken);

        if (stream is null)
        {
            throw new KeyNotFoundException("Document file not found on storage.");
        }

        return new DownloadDocumentResult(
            stream,
            document.ContentType,
            document.FileName);
    }
}
