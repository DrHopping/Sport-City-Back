using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.EventAggregate;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.PlayerAggregate;

public class Player : BaseEntity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    private readonly List<Event> _participatedEvents = new();

    public IReadOnlyCollection<Event> ParticipatedEvents => _participatedEvents.AsReadOnly();

    private readonly List<Event> _organizedEvents = new();
    public IReadOnlyCollection<Event> OrganizedEvents => _organizedEvents.AsReadOnly();

    private Player() { }

    public Player(string identityGuid, string firstName, string lastName, int category) //TODO: Add avatars
    {
        IdentityGuid = identityGuid;
        FirstName = firstName;
        LastName = lastName;
        CategoryId = category;
    }

    public void UpdateCategory(int categoryId) => CategoryId = Guard.Against.Negative(categoryId, nameof(categoryId));

    public void UpdateFirstName(string firstName) =>
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));

    public void UpdateLastName(string lastName) =>
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));

    public void UpdateWith(Player playerUpdate)
    {
        CategoryId = playerUpdate.CategoryId > 0 ? playerUpdate.CategoryId : CategoryId;
        FirstName = !String.IsNullOrWhiteSpace(playerUpdate.FirstName) ? playerUpdate.FirstName : FirstName;
        LastName = !String.IsNullOrWhiteSpace(playerUpdate.LastName) ? playerUpdate.LastName : LastName;
    }
}
