using Ardalis.Specification;

namespace SportCity.Core.Entities.PlayerAggregate.Specifications;

public class PlayerIncludeCategorySpec : Specification<Player>, ISingleResultSpecification
{
  public PlayerIncludeCategorySpec()
  {
    Query
      .Include(p => p.Category);
  }
}
