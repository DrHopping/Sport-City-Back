using System.ComponentModel.DataAnnotations;

namespace SportCity.Web.Models;

public record UserRequest(
  [Required] string FirstName,
  [Required] string LastName,
  [Required] string Password,
  [Required] [EmailAddress] string Email); 

public record UserCreateResponse(string IdentityId);
