using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SportCity.Web.Endpoints.CityEndpoints;

public class Create: EndpointBaseAsync
  .WithRequest<CreateCityRequest>
  .WithActionResult<CreateCityResponse>
{
  private readonly ICityService _cityService;

  public Create(ICityService cityService)
  {
    _cityService = cityService;
  }

  [HttpPost("/cities")]
  [SwaggerOperation(
    Summary = "Creates a new City",
    Description = "Creates a new City",
    OperationId = "City.Create",
    Tags = new[] { "CityEndpoints" })
  ]
  public override async Task<ActionResult<CreateCityResponse>> HandleAsync(CreateCityRequest request, CancellationToken cancellationToken)
  {
    var city = await _cityService.CreateCity(request.Name);
    return Ok(city);
  }
}
