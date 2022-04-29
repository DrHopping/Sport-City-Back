using Ardalis.GuardClauses;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.SportAggregate;

public class Sport : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  public Sport() { }

  public Sport(string name)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));;
  }
  
  public void UpdateName(string name) => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

}
