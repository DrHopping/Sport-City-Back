using Ardalis.GuardClauses;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.CategoryAggregate;

public class Category : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  private Category() { }

  public Category(string name)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));;
  }
  
  public void UpdateName(string name) => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

}
