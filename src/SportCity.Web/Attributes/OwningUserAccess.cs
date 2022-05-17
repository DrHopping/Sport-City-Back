namespace SportCity.Web.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class OwningUserAccess : Attribute
{
  public Type Type { get; set; }

  public OwningUserAccess(Type type)
  {
    Type = type;
  }
}

[AttributeUsage(AttributeTargets.Parameter)]
public class OwningUserAccessId : Attribute
{
  
}
