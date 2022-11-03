using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using SportCity.Core.Exceptions;

namespace SportCity.Core.Guards;

public static class SharedGuards
{
  public static void EntityAlreadyExists<T>(this IGuardClause guardClause, T entity, string field, string value)
  {
    if (entity is not null)
    {
      throw new EntityAlreadyExistsExceptions(typeof(T).Name, field, value);
    }
  }

  public static void EntityNotFound<T>(this IGuardClause guardClause, [NotNull] [ValidatedNotNull] T entity,
    string field, string value)
  {
    if (entity is null)
    {
      throw new EntityNotFound(typeof(T).Name, field, value);
    }
  }
}
