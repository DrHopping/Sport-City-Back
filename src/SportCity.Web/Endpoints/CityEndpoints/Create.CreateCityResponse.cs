namespace SportCity.Web.Endpoints.CityEndpoints;

public class CreateCityResponse
{
  public CreateCityResponse(int id, string name)
  {
    Id = id;
    Name = name;
  }
  public int Id { get; set; }
  public string Name { get; set; }
}
