using Ardalis.Specification;
using SportCity.Core.Entities.CityAggregate;

namespace SportCity.Core.Entities.PlayerAggregate.Specifications;

public class PlayerByAnyIdSpec : Specification<Player>, ISingleResultSpecification
{
  public PlayerByAnyIdSpec(string id)
  {
    Query
      .Where(p => p.Id.ToString() == id || p.IdentityGuid == id)
      .Include(p => p.Category);
  }
}
