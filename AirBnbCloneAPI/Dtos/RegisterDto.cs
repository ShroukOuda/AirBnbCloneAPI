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
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [StringLength(2)]
    public string CountryCode { get; set; }
    
    [Required]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
}