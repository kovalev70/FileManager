namespace FileManager.Application.Interfaces;

public interface IDeleteFileUseCase
{
    Task ExecuteAsync(int fileId);
}
