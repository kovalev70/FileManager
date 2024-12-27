namespace FileManager.Application.Models;

public class FileDownloadResponse
{
    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }
}
