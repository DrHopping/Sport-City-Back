using Ardalis.GuardClauses;
using SportCity.Infrastructure.Exceptions;
using SportCity.Infrastructure.Identity;

namespace SportCity.Infrastructure.Guards;

public static class UserGuards
{
  public static void EmailAlreadyTaken(this IGuardClause guardClause, EfApplicationUser user)
  {
    if (user is not null)
    {
      throw new EmailAlreadyTakenException(user.Email);
    }
  }
  
  public static void UsernameAlreadyTaken(this IGuardClause guardClause, EfApplicationUser user)
  {
    if (user is not null)
    {
      throw new UsernameAlreadyTakenException(user.UserName);
    }
  }
}
