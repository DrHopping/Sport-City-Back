using Ardalis.Specification;
using SportCity.Core.Entities.PlayerAggregate;

namespace SportCity.Core.Entities.PlaygroundAggregate.Specifications;

public class PlaygroundByIdSpec : Specification<Playground>, ISingleResultSpecification
{
    public PlaygroundByIdSpec(int id)
    {
        Query
            .Include(p => p.City)
            .Include(p => p.Reviews).ThenInclude(r => r.Reviewer)
            .Where(p => p.Id == id);

    }
}
