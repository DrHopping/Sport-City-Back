using SportCity.SharedKernel.Exceptions;

namespace SportCity.Core.Exceptions;

public class EntityNotFound : NotFoundException
{
  public EntityNotFound(string entityName, string fieldName, string value) 
    : base($"{entityName} with {fieldName.ToLower()} {value} not found") { }
}
