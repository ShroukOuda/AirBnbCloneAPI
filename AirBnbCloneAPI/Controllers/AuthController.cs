using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirBnbCloneAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public AuthController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User user = new User();
        _mapper.Map(model, user);
        // user.UserName = model.UserName;
        // user.Email = model.Email;
        // user.DateOfBirth = model.BirthDate;
        // user.FirstName = model.FirstName;
        // user.LastName = model.LastName;
        // user.CreatedAt = DateTime.UtcNow;
            
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User Registered Successfully");
    }
}