using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.Guards;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.EventAggregate;

public class Event : BaseEntity, IAggregateRoot
{
    public const int MinCapacity = 2;

    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    public int SportId { get; private set; }
    public Sport Sport { get; private set; }

    public int OrganizerId { get; private set; }
    public Player Organizer { get; private set; }

    public int PlaygroundId { get; private set; }
    public Playground Playground { get; private set; }

    public int Capacity { get; private set; }
    public DateTime DateTime { get; private set; }
    public int ParticipantsCount => _participants.Count;

    private readonly List<Player> _participants = new();
    public IReadOnlyCollection<Player> Participants => _participants.AsReadOnly();

    private Event() { }

    public Event(int categoryId, int sportId, int organizerId, int playgroundId, int capacity, DateTime dateTime)
    {
        CategoryId = categoryId;
        SportId = sportId;
        OrganizerId = organizerId;
        PlaygroundId = playgroundId;
        Capacity = capacity;
        DateTime = dateTime;
    }

    public void AddParticipant(Player player)
    {
        Guard.Against.FullEvent(_participants.Count, Capacity);
        _participants.Add(player);
    }

    public void RemoveParticipant(Player player)
    {
        _participants.Remove(player);
    }
}
