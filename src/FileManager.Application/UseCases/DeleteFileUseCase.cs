namespace FileManager.Application.UseCases;

public class DeleteFileUseCase : IDeleteFileUseCase
{
    private readonly IFileService _fileService;

    public DeleteFileUseCase(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task ExecuteAsync(int fileId)
    {
        await _fileService.DeleteFileAsync(fileId);
    }
}
