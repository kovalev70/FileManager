namespace FileManager.Application.UseCases;

public class UploadFileUseCase : IUploadFileUseCase
{
    private readonly IFileService _fileService;

    public UploadFileUseCase(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<FileDto> ExecuteAsync(string userName, string fileName, Stream fileStream)
    {
        return await _fileService.UploadFileAsync(userName, fileName, fileStream);
    }
}
