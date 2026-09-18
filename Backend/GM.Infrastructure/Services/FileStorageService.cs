using GM.Application.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;

namespace GM.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _imagesPath;

    public FileStorageService(
        IWebHostEnvironment environment)
    {
        _imagesPath = Path.Combine(
            environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot"),
            "images",
            "products");
    }

    public async Task<string> SaveAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_imagesPath);

        var extension = Path.GetExtension(fileName);

        var newFileName =
            $"{Guid.NewGuid()}{extension}";

        var fullPath = Path.Combine(
            _imagesPath,
            newFileName);

        await using var outputStream =
            new FileStream(
                fullPath,
                FileMode.Create);

        await fileStream.CopyToAsync(
            outputStream,
            cancellationToken);

        return $"/images/products/{newFileName}";
    }


    public Task DeleteAsync(
        string filePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return Task.CompletedTask;
        }

        var relativePath = filePath
            .TrimStart('/')
            .Replace(
                '/',
                Path.DirectorySeparatorChar);

        var fullPath = Path.Combine(
            Directory.GetParent(_imagesPath)!.Parent!.FullName,
            relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}