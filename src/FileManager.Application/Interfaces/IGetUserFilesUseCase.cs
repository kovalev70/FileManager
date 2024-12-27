namespace FileManager.Application.Interfaces;

public interface IGetUserFilesUseCase
{
    Task<IEnumerable<FileDto>> ExecuteAsync(string userName);
}
