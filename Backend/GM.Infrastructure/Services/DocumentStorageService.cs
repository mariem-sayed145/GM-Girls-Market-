using GM.Application.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;

namespace GM.Infrastructure.Services;

public class DocumentStorageService : IDocumentStorageService
{
    private readonly string _storageDirectory;

    public DocumentStorageService(IWebHostEnvironment environment)
    {
        _storageDirectory = Path.Combine(
            environment.ContentRootPath,
            "App_Data",
            "Documents");
    }

    public async Task<(string StoredFileName, string StoragePath)> SaveDocumentAsync(
        Stream fileStream,
        string originalFileName,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_storageDirectory);

        var extension = Path.GetExtension(originalFileName).ToLowerInvariant();
        var uniqueName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(_storageDirectory, uniqueName);

        // Verify path stays within storage directory
        EnsureSafePath(fullPath);

        await using var outputStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(outputStream, cancellationToken);

        return (uniqueName, uniqueName);
    }

    public Task<Stream?> GetDocumentStreamAsync(
        string storagePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(storagePath))
        {
            return Task.FromResult<Stream?>(null);
        }

        var fullPath = Path.Combine(_storageDirectory, Path.GetFileName(storagePath));
        EnsureSafePath(fullPath);

        if (!File.Exists(fullPath))
        {
            return Task.FromResult<Stream?>(null);
        }

        Stream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return Task.FromResult<Stream?>(fileStream);
    }

    public Task DeleteDocumentAsync(
        string storagePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(storagePath))
        {
            return Task.CompletedTask;
        }

        var fullPath = Path.Combine(_storageDirectory, Path.GetFileName(storagePath));
        EnsureSafePath(fullPath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    private void EnsureSafePath(string targetPath)
    {
        var normalizedTarget = Path.GetFullPath(targetPath);
        var normalizedRoot = Path.GetFullPath(_storageDirectory);

        if (!normalizedTarget.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid storage path.");
        }
    }
}
