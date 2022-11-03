using Ardalis.Specification;
using SportCity.Core.Entities.PlayerAggregate;

namespace SportCity.Core.Entities.PlaygroundAggregate.Specifications;

public class PlaygroundIncludeCitySpec : Specification<Playground>, ISingleResultSpecification
{
    public PlaygroundIncludeCitySpec()
    {
        Query
            .Include(p => p.City);
    }
}
