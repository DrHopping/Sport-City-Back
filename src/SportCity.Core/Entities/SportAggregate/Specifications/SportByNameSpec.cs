using Ardalis.Specification;
using SportCity.Core.Entities.CityAggregate;

namespace SportCity.Core.Entities.SportAggregate.Specifications;

public class SportByNameSpec : Specification<Sport>, ISingleResultSpecification
{
  public SportByNameSpec(string name)
  {
    Query
      .Where(c => c.Name.ToLower() == name.ToLower());
  }
}
