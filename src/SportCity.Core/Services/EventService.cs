using Ardalis.GuardClauses;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.EventAggregate.Specifications;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class EventService: IEventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<Player> _playerRepository;

    public EventService(IRepository<Event> eventRepository, IRepository<Player> playerRepository)
    {
        _eventRepository = eventRepository;
        _playerRepository = playerRepository;
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

    public async Task<Event> AddParticipant(int eventId, int playerId)
    {
        var @event = await _eventRepository.GetBySpecAsync(new EventByIdSpec(eventId));
        Guard.Against.EntityNotFound(@event, nameof(eventId), eventId.ToString());

        var player = await _playerRepository.GetByIdAsync(playerId);
        Guard.Against.EntityNotFound(player, nameof(playerId), playerId.ToString());

        @event.AddParticipant(player);

        await _eventRepository.SaveChangesAsync();

        return @event;
    }

    public async Task<Event> RemoveParticipant(int eventId, int playerId)
    {
        var @event = await _eventRepository.GetBySpecAsync(new EventByIdSpec(eventId));
        Guard.Against.EntityNotFound(@event, nameof(eventId), eventId.ToString());

        var player = await _playerRepository.GetByIdAsync(playerId);
        Guard.Against.EntityNotFound(player, nameof(playerId), playerId.ToString());

        @event.RemoveParticipant(player);

        await _eventRepository.SaveChangesAsync();

        return @event;
    }
}

public interface IEventService
{
    Task<List<Event>> GetAllEvents();
    Task<List<Event>> GetCityEvents(int cityId);
    Task<List<Event>> GetPlaygroundEvents(int playgroundId);
    Task<Event> GetEvent(int id);
    Task<Event> AddParticipant(int eventId, int playerId);
    Task<Event> RemoveParticipant(int eventId, int playerId);
}
