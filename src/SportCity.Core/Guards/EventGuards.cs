using Ardalis.GuardClauses;
using SportCity.Core.Entities.EventAggregate.Exceptions;

namespace SportCity.Core.Guards;

public static class EventGuards
{
    public static void FullEvent(this IGuardClause guardClause, int participants, int capacity)
    {
        if (participants == capacity)
        {
            throw new EventIsFullExceptions(capacity);
        }
    }
}
