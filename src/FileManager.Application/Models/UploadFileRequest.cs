namespace FileManager.Application.Models;

public class UploadFileRequest
{
    [FromForm]
    public IFormFile? File { get; set; }
}
