using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
  private readonly ICityService _cityService;
  private readonly IMapper _mapper;

  public CitiesController(ICityService cityService, IMapper mapper)
  {
    _cityService = cityService;
    _mapper = mapper;
  }
  
  [HttpPost]
  public async Task<IActionResult> CreateCity([FromBody] CityRequest request, CancellationToken cancellationToken = new())
  {
    var city = await _cityService.CreateCity(request.Name);
    return Ok(_mapper.Map<CityResponse>(city));
  }
 
  [HttpGet]
  public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken = new())
  {
    var cities = await _cityService.GetAllCities();
    return Ok(_mapper.Map<List<CityResponse>>(cities));
  }
  
  [HttpPatch]
  [Route("{id:int}")]
  public async Task<IActionResult> UpdateCity(int id, [FromBody] CityRequest request, CancellationToken cancellationToken = new())
  {
    var city = await _cityService.UpdateCityName(id, request.Name);
    return Ok(_mapper.Map<CityResponse>(city));
  }
  
  [HttpDelete]
  [Route("{id:int}")]
  public async Task<IActionResult> DeleteCity(int id, CancellationToken cancellationToken = new())
  {
    await _cityService.DeleteCity(id);
    return NoContent();
  }
  
}
