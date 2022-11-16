using Ardalis.Specification;

namespace SportCity.Core.Entities.PlaygroundAggregate.Specifications;

class PlaygroundListByCitySpec : PlaygroundListSpec
{
    public PlaygroundListByCitySpec(int cityId) : base()
    {
        Query
            .Where(p => p.CityId == cityId);
    }
}
