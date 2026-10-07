using Microsoft.AspNetCore.Http;

namespace GM.API.Models.Documents;

public class UploadDocumentRequest
{
    public IFormFile File { get; set; } = null!;
}
