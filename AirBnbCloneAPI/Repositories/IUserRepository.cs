using Microsoft.AspNetCore.Identity;

namespace AirBnbCloneAPI.Repositories;

public interface IUserRepository
{
    Task<User> GetByUserNameAsync(string userName);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByPhoneAsync(string phoneNumber);
    Task<IdentityResult> CreateUserAsync(User user, string  password);
    Task<bool> CheckPasswordAsync(User user, string password);
}