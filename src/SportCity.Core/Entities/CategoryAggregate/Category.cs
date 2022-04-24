using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.CategoryAggregate;

public class Category : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  private Category() { }

  public Category(string name)
  {
    Name = name;
  }
}
