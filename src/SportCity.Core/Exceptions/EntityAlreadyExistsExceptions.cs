namespace SportCity.Core.Exceptions;

public class EntityAlreadyExistsExceptions : Exception
{
  public EntityAlreadyExistsExceptions(string entityName, string fieldName, string value) 
    : base($"{entityName} with {fieldName.ToLower()} {value} already exist") { }
}
