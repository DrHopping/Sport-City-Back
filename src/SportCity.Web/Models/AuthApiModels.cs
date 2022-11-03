using System.ComponentModel.DataAnnotations;

namespace SportCity.Web.Models;

public record AuthRequest([Required] [EmailAddress] string Email, [Required] string Password);

public record AuthResponse(string Token);
