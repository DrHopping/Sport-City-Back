using Ardalis.Specification;

namespace SportCity.Core.Entities.PlaygroundAggregate.Specifications;

public class PlaygroundListSpec : Specification<Playground>
{
    public PlaygroundListSpec()
    {
        Query
            .Include(p => p.City)
            .Include(p => p.Reviews);
    }
}
