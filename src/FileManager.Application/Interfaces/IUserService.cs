namespace FileManager.Application.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string userName);
    Task CreateUserAsync(string userName);
}
