namespace FileManager.Application.Interfaces;

public interface IFileService
{
    Task<IEnumerable<FileDto>> GetUserFilesAsync(string userName);
    Task<FileDto> UploadFileAsync(string userName, string fileName, Stream fileStream);
    Task DeleteFileAsync(int fileId);
    Task<FileDto> GetFileByIdAsync(int fileId);
}