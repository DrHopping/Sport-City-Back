using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using SportCity.Infrastructure.Exceptions;
using SportCity.Infrastructure.Identity;
using SportCity.SharedKernel.Exceptions;

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
  
  public static void BadPassword(this IGuardClause guardClause, IdentityResult result)
  {
    if (result.Errors.Any())
    {
      var errors = result.Errors.Select(e => e.Description).ToArray();
      throw new BadRequestException(errors);
    }
  }
  
  public static void WrongPassword(this IGuardClause guardClause, bool result)
  {
    if (!result)
    {
      throw new WrongPasswordException();
    }
  }
}
