namespace SportCity.Core.Entities.PlaygroundAggregate;

public class Location // ValueObject
{
  public string Address { get; private set; }
  public double Longitude { get; private set; }
  public double Latitude { get; private set; }

  private Location() { }

  public Location(string address, double longitude, double latitude)
  {
    Address = address;
    Longitude = longitude;
    Latitude = latitude;
  }

}
