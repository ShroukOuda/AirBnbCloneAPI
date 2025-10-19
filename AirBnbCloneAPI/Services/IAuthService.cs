namespace AirBnbCloneAPI.Services;

public interface IAuthService
{
    Task<(bool Success, string Message)> RegisterAsync(RegisterDto model);
    Task<(bool Success, string Message)> LoginAsync(LoginDto model);
}