namespace FileManager.Application.UseCases;

public class GetUserFilesUseCase : IGetUserFilesUseCase
{
    private readonly IFileService _fileService;

    public GetUserFilesUseCase(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<IEnumerable<FileDto>> ExecuteAsync(string userName)
    {
        return await _fileService.GetUserFilesAsync(userName);
    }
}
