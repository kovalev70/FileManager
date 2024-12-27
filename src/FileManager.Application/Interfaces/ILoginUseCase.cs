namespace FileManager.Application.Interfaces;

public interface ILoginUseCase
{
    Task<(bool Success, string UserName)> ExecuteAsync(string userName);
}
