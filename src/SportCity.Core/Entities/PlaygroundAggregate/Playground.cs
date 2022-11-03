using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.EventAggregate;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.PlaygroundAggregate;

public class Playground : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Location Location { get; private set; }
    public int CityId { get; set; }
    public City City { get; private set; }

    public double Rating => _reviews.Select(r => r.Rating).DefaultIfEmpty(0).Average();

    private readonly List<Review> _reviews = new();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private readonly List<Event> _events = new();
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    private Playground() { }

    public Playground(string name, string description, int city, Location location)
    {
        Name = name;
        Description = description;
        CityId = city;
        Location = location;
    }

    public void UpdateWith(Playground playgroundUpdate)
    {
        Name = !String.IsNullOrWhiteSpace(playgroundUpdate.Name) ? playgroundUpdate.Name : Name;
        Description = !String.IsNullOrWhiteSpace(playgroundUpdate.Description)
            ? playgroundUpdate.Description
            : Description;
        CityId = playgroundUpdate.CityId > 0 ? playgroundUpdate.CityId : CityId;
        Location = playgroundUpdate.Location ?? Location;
    }
}
