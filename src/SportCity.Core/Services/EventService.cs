using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.EventAggregate.Specifications;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class EventService: IEventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<Player> _playerRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Sport> _sportRepository;
    private readonly IRepository<Playground> _playgroundRepository;

    public EventService(
        IRepository<Event> eventRepository,
        IRepository<Player> playerRepository,
        IRepository<Category> categoryRepository,
        IRepository<Sport> sportRepository,
        IRepository<Playground> playgroundRepository)
    {
        _eventRepository = eventRepository;
        _playerRepository = playerRepository;
        _categoryRepository = categoryRepository;
        _sportRepository = sportRepository;
        _playgroundRepository = playgroundRepository;
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

    public async Task<Event> CreateEvent(int categoryId, int sportId, int organizerId, int playgroundId, int capacity,
        DateTime dateTime)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        Guard.Against.EntityNotFound(category, nameof(categoryId), categoryId.ToString());

        var sport = await _sportRepository.GetByIdAsync(sportId);
        Guard.Against.EntityNotFound(sport, nameof(sportId), sportId.ToString());

        var organizer = await _playerRepository.GetByIdAsync(organizerId);
        Guard.Against.EntityNotFound(organizer, nameof(organizerId), organizerId.ToString());

        var playground = await _playgroundRepository.GetByIdAsync(playgroundId);
        Guard.Against.EntityNotFound(playground, nameof(playgroundId), playgroundId.ToString());

        Guard.Against.LessThan(capacity, Event.MinCapacity, nameof(capacity));

        Guard.Against.Past(dateTime, nameof(dateTime));

        var @event = await _eventRepository.AddAsync(
            new Event(categoryId, sportId, organizerId, playgroundId, capacity, dateTime));

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
    Task<Event> CreateEvent(int categoryId, int sportId, int organizerId, int playgroundId, int capacity,
        DateTime dateTime);
    Task<Event> AddParticipant(int eventId, int playerId);
    Task<Event> RemoveParticipant(int eventId, int playerId);
}
