using Ardalis.Specification;

namespace SportCity.Core.Entities.EventAggregate.Specifications;

public class EventByIdSpec : Specification<Event>, ISingleResultSpecification<Event>
{
    public EventByIdSpec(int id)
    {
        Query
            .Include(e => e.Sport)
            .Include(e => e.Category)
            .Include(e => e.Playground).ThenInclude(p => p.City)
            .Include(e => e.Participants);
    }
}

public class EventIncludesSpec : Specification<Event>
{
    public EventIncludesSpec()
    {
        Query
            .Include(e => e.Sport)
            .Include(e => e.Category)
            .Include(e => e.Playground).ThenInclude(p => p.City)
            .Include(e => e.Participants);
    }
}
