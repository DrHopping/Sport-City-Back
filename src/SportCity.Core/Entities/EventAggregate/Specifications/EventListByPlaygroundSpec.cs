using Ardalis.Specification;

namespace SportCity.Core.Entities.EventAggregate.Specifications;

public class EventListByPlaygroundSpec : EventListSpec
{
    public EventListByPlaygroundSpec(int playgroundId) : base()
    {
        Query
            .Where(e => e.Playground.Id == playgroundId);
    }
}
