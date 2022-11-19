using Ardalis.Specification;

namespace SportCity.Core.Entities.EventAggregate.Specifications;

public class EventByIdSpec : EventBaseSpec, ISingleResultSpecification<Event>
{
    public EventByIdSpec(int id) : base()
    {
        Query
            .Include(e => e.Organizer)
            .Where(e => e.Id == id);
    }
}
