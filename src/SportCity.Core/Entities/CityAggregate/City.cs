using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.CityAggregate;

public class City : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  private City() { }

  public City(string name)
  {
    Name = name;
  }

}
