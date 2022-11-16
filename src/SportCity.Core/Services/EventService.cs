using Ardalis.GuardClauses;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.EventAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class EventService: IEventService
{
    private readonly IRepository<Event> _eventRepository;

    public EventService(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<Event>> GetAllEvents() => await _eventRepository.ListAsync(new EventIncludesSpec());

    public async Task<Event> GetEvent(int id)
    {
        var @event = await _eventRepository.GetBySpecAsync(new EventByIdSpec(id));
        Guard.Against.EntityNotFound(@event, nameof(id), id.ToString());
        return @event;
    }

}

public interface IEventService
{
    Task<List<Event>> GetAllEvents();
    Task<Event> GetEvent(int id);
}
