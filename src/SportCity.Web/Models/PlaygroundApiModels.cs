using System.ComponentModel.DataAnnotations;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;

namespace SportCity.Web.Models;

public record PlaygroundRequest(
    [Required] string Name,
    [Required] string Description,
    [Required] int CityId,
    [Required] string PhotoUrl,
    [Required] Location Location);

public record PlaygroundCreateResponse(int Id, string Name, string Description, int CityId, Location Location);

public record PlaygroundResponse(int Id, string Name, string Description, double Rating, string PhotoUrl, City City,
    List<Review> Reviews, Location Location);

public record PlaygroundListResponse(int Id, string Name, string Description, double Rating, string PhotoUrl, City City,
    Location Location);
