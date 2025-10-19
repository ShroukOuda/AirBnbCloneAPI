using System.ComponentModel.DataAnnotations;

namespace AirBnbCloneAPI.Dtos;

public class LoginDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}