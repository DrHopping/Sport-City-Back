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

    public async Task<List<Event>> GetAllEvents()
        => await _eventRepository.ListAsync(new EventListSpec());

    public async Task<List<Event>> GetCityEvents(int cityId)
        => await _eventRepository.ListAsync(new EventListByCitySpec(cityId));

    public async Task<List<Event>> GetPlaygroundEvents(int playgroundId)
        => await _eventRepository.ListAsync(new EventListByPlaygroundSpec(playgroundId));

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
    Task<List<Event>> GetCityEvents(int cityId);
    Task<List<Event>> GetPlaygroundEvents(int playgroundId);
    Task<Event> GetEvent(int id);
}
