using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.SportAggregate;

public class Sport : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  private Sport() { }

  public Sport(string name)
  {
    Name = name;
  }

}
