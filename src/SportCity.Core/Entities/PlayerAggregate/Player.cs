using SportCity.Core.Entities.CategoryAggregate;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.PlayerAggregate;

public class Player : BaseEntity, IAggregateRoot
{
  public string IdentityGuid { get; private set; }
  public byte[] Avatar { get; private set; }
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public int CategoryId { get; private set; }
  public Category Category { get; private set; }
  
  private Player() { }

  public Player(string identityGuid, byte[] avatar, string firstName, string lastName, int category)
  { 
    IdentityGuid = identityGuid;
    Avatar = avatar;
    FirstName = firstName;
    LastName = lastName;
    CategoryId = category;
  }
}
