using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models;

public class ApplicationUser
{
    public int Id { get; set; }
    [Required] 
    public string Username { get; set; } = string.Empty;
    [Required] 
    public string Password { get; set; } = string.Empty;
    [Required] 
    public string Email { get; set; } = string.Empty;
}

