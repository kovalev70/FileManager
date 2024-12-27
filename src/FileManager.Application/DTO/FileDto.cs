namespace FileManager.Application.DTO;

public class FileDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string Hash { get; set; }
    public DateTime UploadedAt { get; set; }
}
