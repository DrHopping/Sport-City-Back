using Ardalis.Specification;

namespace SportCity.Core.Entities.CityAggregate.Specifications;

public class CityByNameSpec : Specification<City>, ISingleResultSpecification
{
  public CityByNameSpec(string name)
  {
    Query
      .Where(c => c.Name.ToLower() == name.ToLower());
  }
}
