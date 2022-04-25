using Ardalis.GuardClauses;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.CityAggregate;

public class City : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  private City() { }

  public City(string name)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));;
  }

  public void UpdateName(string name) => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

}
