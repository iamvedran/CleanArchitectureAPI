using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Application.Models.Identity;

public class AuthRequest
{
    /// <summary>
    /// User email
    /// </summary>
    /// <example>vedran@gmail.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    /// /// <example>123456</example>
    [Required]
    public string Password { get; set; }
}