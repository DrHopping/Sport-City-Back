namespace SportCity.Core.Exceptions;

public class EntityNotFound : Exception
{
  public EntityNotFound(string entityName, string fieldName, string value) 
    : base($"{entityName} with {fieldName.ToLower()} {value} not found") { }
}
