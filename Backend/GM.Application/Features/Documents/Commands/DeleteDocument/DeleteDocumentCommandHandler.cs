using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using MediatR;

namespace GM.Application.Features.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
{
    private readonly IDocumentRepository _repository;
    private readonly IDocumentStorageService _storageService;

    public DeleteDocumentCommandHandler(
        IDocumentRepository repository,
        IDocumentStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    public async Task Handle(
        DeleteDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (document is null)
        {
            throw new KeyNotFoundException("Document not found.");
        }

        if (document.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this document.");
        }

        await _storageService.DeleteDocumentAsync(document.StoragePath, cancellationToken);
        await _repository.DeleteAsync(document, cancellationToken);
    }
}
