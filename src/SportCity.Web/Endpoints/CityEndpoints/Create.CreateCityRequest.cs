using System.ComponentModel.DataAnnotations;

namespace SportCity.Web.Endpoints.CityEndpoints;

public class CreateCityRequest
{
  [Required] public string Name { get; set; }
}
