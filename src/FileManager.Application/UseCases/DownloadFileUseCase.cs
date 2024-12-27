namespace FileManager.Application.UseCases;

public class DownloadFileUseCase : IDownloadFileUseCase
{
    private readonly IFileService _fileService;

    public DownloadFileUseCase(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<FileDownloadResponse> ExecuteAsync(int fileId)
    {
        var file = await _fileService.GetFileByIdAsync(fileId);
        if (file == null)
        {
            return null;
        }

        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(file.FilePath);

        return new FileDownloadResponse
        {
            FileName = file.FileName,
            FileContent = fileBytes
        };
    }
}
