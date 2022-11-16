using Ardalis.Specification;

namespace SportCity.Core.Entities.EventAggregate.Specifications;

public class EventBaseSpec : Specification<Event>
{
    public EventBaseSpec()
    {
        Query
            .Include(e => e.Sport)
            .Include(e => e.Category)
            .Include(e => e.Participants)
            .Include(e => e.Playground).ThenInclude(p => p.City);
    }
}
