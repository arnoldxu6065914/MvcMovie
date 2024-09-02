using System.ComponentModel.DataAnnotations;
using MvcMovie.Models;

namespace MvcMovie.Models;

public class ApplicationUser
{
    public int Id { get; set; }
    [Required] 
    public required string Username { get; set; } 
    [Required] 
    public required string Password { get; set; } 
    [Required] 
    public required string Email { get; set; }
}

