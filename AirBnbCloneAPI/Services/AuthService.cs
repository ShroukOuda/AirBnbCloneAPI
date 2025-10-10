using AirBnbCloneAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PhoneNumbers;
namespace AirBnbCloneAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public AuthService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    private bool IsValidPhoneNumber(string phone, string  region)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var numberProto = phoneUtil.Parse(phone, region);
            return phoneUtil.IsValidNumber(numberProto);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto model)
    {
        if (string.IsNullOrWhiteSpace(model.CountryCode) || 
            model.CountryCode.Length != 2 || 
            !model.CountryCode.All(char.IsUpper))
        {
            return (false, "Country code must be exactly 2 uppercase letters.");
        }

        if (!IsValidPhoneNumber(model.PhoneNumber, model.CountryCode))
        {
            return (false, "Invalid phone number for the selected country.");
        }
        
        var existingEmail = await _userRepository.GetByEmailAsync(model.Email);
        if (existingEmail != null)
        {
            return (false, "Email already exists.");
        }

        var existingPhone = await _userRepository.GetByPhoneAsync(model.PhoneNumber);
        if (existingPhone != null)
        {
            return (false, "Phone number already exists.");
        }

        var user = new User();
        _mapper.Map(model, user);
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        
        IdentityResult result = await _userRepository.CreateUserAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        
        return (true, "User Registered Successfully.");
    }
}