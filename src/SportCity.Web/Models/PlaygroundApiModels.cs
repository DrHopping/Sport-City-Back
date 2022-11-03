using System.ComponentModel.DataAnnotations;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;

namespace SportCity.Web.Models;

public record PlaygroundRequest([Required] string Name, [Required] string Description, [Required] int CityId,
    [Required] Location Location);

public record PlaygroundCreateResponse(int Id, string Name, string Description, int CityId, Location Location);

public record PlaygroundGetResponse(int Id, string Name, string Description, double Rating, City City,
    Location Location);
