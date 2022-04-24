using Ardalis.GuardClauses;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.EventAggregate.Exceptions;
using SportCity.Core.Exceptions;

namespace SportCity.Core.Guards;

public static class CityGuards
{
  public static void ExistingCity(this IGuardClause guardClause, City city, string name)
  {
    if (city is not null)
    {
      throw new EntityAlreadyExistsExceptions(nameof(City), nameof(City.Name), name);
    }
  }
}
