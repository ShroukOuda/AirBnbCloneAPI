using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AirBnbCloneAPI.Models;

public class User : IdentityUser
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters")]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters")]
    public string LastName { get; set; }
    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }
    [Column(TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }
    [Column(TypeName = "datetime2")]
    public DateTime? UpdatedAt { get; set; }
}