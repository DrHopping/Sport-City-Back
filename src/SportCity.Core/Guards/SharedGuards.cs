using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using SportCity.Core.Exceptions;
using SportCity.SharedKernel.Exceptions;

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

    public static void LessThan(this IGuardClause guardClause, int value, int min, string field)
    {
        if (value < min)
        {
            throw new BadRequestException($"{field} should be greater than {min}. Was {value}.");
        }
    }

    public static void Past(this IGuardClause guardClause, DateTime value, string field)
    {
        if (value < DateTime.Now)
        {
            throw new BadRequestException($"{field} should be in future. Was {value}.");
        }
    }
}
