using Ardalis.Specification;

namespace SportCity.Core.Entities.PlaygroundAggregate.Specifications;

public class PlaygroundListIncludeSpec : Specification<Playground>
{
    public PlaygroundListIncludeSpec()
    {
        Query
            .Include(p => p.City)
            .Include(p => p.Reviews);
    }
}
