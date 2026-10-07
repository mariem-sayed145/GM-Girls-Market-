namespace GM.Application.Abstractions.Services;

public interface IDocumentStorageService
{
    Task<(string StoredFileName, string StoragePath)> SaveDocumentAsync(
        Stream fileStream,
        string originalFileName,
        CancellationToken cancellationToken = default);

    Task<Stream?> GetDocumentStreamAsync(
        string storagePath,
        CancellationToken cancellationToken = default);

    Task DeleteDocumentAsync(
        string storagePath,
        CancellationToken cancellationToken = default);
}
