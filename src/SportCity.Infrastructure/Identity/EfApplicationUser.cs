using Microsoft.AspNetCore.Identity;

namespace SportCity.Infrastructure.Identity;

public class EfApplicationUser : IdentityUser
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
