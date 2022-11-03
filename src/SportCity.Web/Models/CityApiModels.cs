using System.ComponentModel.DataAnnotations;

namespace SportCity.Web.Models;

public record CityRequest([Required] string Name);

public record CityResponse(int Id, string Name);
