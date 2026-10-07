using GM.Domain.Common;

namespace GM.Domain.Entities;

public class Document : BaseEntity
{
    public int UserId { get; private set; }

    public string FileName { get; private set; } = null!;

    public string StoredFileName { get; private set; } = null!;

    public string ContentType { get; private set; } = null!;

    public long FileSizeBytes { get; private set; }

    public string StoragePath { get; private set; } = null!;

    private Document()
    {
        // Required by EF Core
    }

    public Document(
        int userId,
        string fileName,
        string storedFileName,
        string contentType,
        long fileSizeBytes,
        string storagePath)
    {
        UserId = userId;
        FileName = fileName;
        StoredFileName = storedFileName;
        ContentType = contentType;
        FileSizeBytes = fileSizeBytes;
        StoragePath = storagePath;
        CreatedAt = DateTime.UtcNow;
    }
}
