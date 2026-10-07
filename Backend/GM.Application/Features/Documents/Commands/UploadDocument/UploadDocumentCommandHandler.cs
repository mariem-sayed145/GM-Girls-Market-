using GM.Application.Abstractions.Persistence;
using GM.Application.Abstractions.Services;
using GM.Domain.Entities;
using MediatR;

namespace GM.Application.Features.Documents.Commands.UploadDocument;

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, int>
{
    private readonly IDocumentRepository _repository;
    private readonly IDocumentStorageService _storageService;

    public UploadDocumentCommandHandler(
        IDocumentRepository repository,
        IDocumentStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    public async Task<int> Handle(
        UploadDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var (storedFileName, storagePath) = await _storageService.SaveDocumentAsync(
            request.FileStream,
            request.FileName,
            cancellationToken);

        var document = new Document(
            request.UserId,
            request.FileName,
            storedFileName,
            request.ContentType,
            request.FileSizeBytes,
            storagePath);

        await _repository.AddAsync(document, cancellationToken);

        return document.Id;
    }
}
