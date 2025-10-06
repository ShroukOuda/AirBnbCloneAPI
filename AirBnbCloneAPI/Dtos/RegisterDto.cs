using System.ComponentModel.DataAnnotations;

namespace AirBnbCloneAPI.Dtos;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string UserName { get; set; }
    
    public string PhoneNumber { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be 8 characters or less")]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
}