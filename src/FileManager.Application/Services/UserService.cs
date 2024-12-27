namespace FileManager.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> UserExistsAsync(string userName)
    {
        var existingUser = await _userRepository.GetAsync(u => u.UserName == userName);
        return existingUser != null;
    }

    public async Task CreateUserAsync(string userName)
    {
        var newUser = new User { UserName = userName };
        await _userRepository.AddAsync(newUser);
    }
}
