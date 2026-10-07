using GM.Application.Abstractions.Persistence;
using GM.Application.Features.Documents.DTOs;
using MediatR;

namespace GM.Application.Features.Documents.Queries.GetMyDocuments;

public class GetMyDocumentsQueryHandler : IRequestHandler<GetMyDocumentsQuery, IReadOnlyList<DocumentDto>>
{
    private readonly IDocumentRepository _repository;

    public GetMyDocumentsQueryHandler(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DocumentDto>> Handle(
        GetMyDocumentsQuery request,
        CancellationToken cancellationToken)
    {
        var documents = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);

        return documents
            .Select(d => new DocumentDto(
                d.Id,
                d.UserId,
                d.FileName,
                d.ContentType,
                d.FileSizeBytes,
                d.CreatedAt))
            .ToList();
    }
}
