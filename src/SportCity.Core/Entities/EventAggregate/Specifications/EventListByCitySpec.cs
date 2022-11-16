using Ardalis.Specification;

namespace SportCity.Core.Entities.EventAggregate.Specifications;

public class EventListByCitySpec : EventListSpec
{
    public EventListByCitySpec(int cityId) : base()
    {
        Query
            .Where(e => e.Playground.CityId == cityId);
    }
}
