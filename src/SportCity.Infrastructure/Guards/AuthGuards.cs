using Ardalis.GuardClauses;
using SportCity.Core.Interfaces;
using SportCity.Infrastructure.Exceptions;
using SportCity.Infrastructure.Identity;
using SportCity.SharedKernel.Exceptions;

namespace SportCity.Infrastructure.Guards;

public static class AuthGuards
{
    public static void NotAdmin(this IGuardClause guardClause, IAuthorizationService service)
    {
        if (!service.IsAdmin())
        {
            throw new RequireAdminRoleException();
        }
    }

    public static void InvalidToken(this IGuardClause guardClause, bool isValid)
    {
        if (!isValid)
        {
            throw new UnauthorizedException("Invalid token");
        }
    }
}
