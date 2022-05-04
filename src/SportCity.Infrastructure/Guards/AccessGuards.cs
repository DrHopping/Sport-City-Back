using Ardalis.GuardClauses;
using SportCity.Core.Interfaces;
using SportCity.Infrastructure.Exceptions;
using SportCity.Infrastructure.Identity;

namespace SportCity.Infrastructure.Guards;

public static class AccessGuards
{
  public static void NotAdmin(this IGuardClause guardClause, IAuthorizationService service)
  {
    if (!service.IsAdmin())
    {
      throw new RequireAdminRoleException();
    }
  }
}
