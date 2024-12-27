namespace FileManager.Application.UseCases;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserService _userService;

    public LoginUseCase(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<(bool Success, string UserName)> ExecuteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return (false, null);
        }

        if (!await _userService.UserExistsAsync(userName))
        {
            await _userService.CreateUserAsync(userName);
        }

        return (true, userName);
    }
}
