[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    public AccountController(ILoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpGet("login")]
    [Authorize(AuthenticationSchemes = "Windows")]
    public async Task<IActionResult> Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userName = User.Identity.Name;

            var (success, resultUserName) = await _loginUseCase.ExecuteAsync(userName);

            if (success)
            {
                return Ok(new { UserName = resultUserName });
            }
        }

        return Unauthorized();
    }
}
