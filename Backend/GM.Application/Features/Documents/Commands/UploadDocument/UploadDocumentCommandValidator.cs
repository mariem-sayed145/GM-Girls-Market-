using FluentValidation;

namespace GM.Application.Features.Documents.Commands.UploadDocument;

public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
{
    private static readonly string[] AllowedExtensions =
    {
        ".pdf", ".doc", ".docx", ".txt", ".jpg", ".jpeg", ".png", ".xlsx"
    };

    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB

    public UploadDocumentCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User ID is invalid.");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required.")
            .MaximumLength(255)
            .WithMessage("File name must not exceed 255 characters.")
            .Must(HaveAllowedExtension)
            .WithMessage("File extension is not supported. Allowed extensions: " + string.Join(", ", AllowedExtensions));

        RuleFor(x => x.ContentType)
            .NotEmpty()
            .WithMessage("Content type is required.")
            .MaximumLength(100)
            .WithMessage("Content type must not exceed 100 characters.");

        RuleFor(x => x.FileSizeBytes)
            .GreaterThan(0)
            .WithMessage("File cannot be empty.")
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage("File size exceeds the maximum allowed limit of 10 MB.");
    }

    private static bool HaveAllowedExtension(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return false;
        }

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return AllowedExtensions.Contains(extension);
    }
}
