using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirBnbCloneAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok("✅ You accessed a protected endpoint!");
    }

    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("🌍 Public endpoint — no token required.");
    }
    
    
}