namespace FileManager.Application.Interfaces;

public interface IDownloadFileUseCase
{
    Task<FileDownloadResponse> ExecuteAsync(int fileId);
}
