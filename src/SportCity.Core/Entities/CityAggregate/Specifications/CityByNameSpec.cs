using Ardalis.Specification;

namespace SportCity.Core.Entities.CityAggregate.Specifications;

public class CityByNameSpec : Specification<City>, ISingleResultSpecification
{
  public CityByNameSpec(string name)
  {
    Query
      .Where(c => String.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));
  }
}
