namespace FileManager.Application.Interfaces;

public interface IUploadFileUseCase
{
    Task<FileDto> ExecuteAsync(string userName, string fileName, Stream fileStream);
}
