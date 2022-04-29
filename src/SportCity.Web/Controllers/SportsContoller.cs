using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SportsController : ControllerBase
{
  private readonly ISportService _sportService;
  private readonly IMapper _mapper;

  public SportsController(ISportService sportService, IMapper mapper)
  {
    _sportService = sportService;
    _mapper = mapper;
  }
  
  [HttpPost]
  public async Task<IActionResult> CreateSport([FromBody] SportRequest request, CancellationToken cancellationToken = new())
  {
    var sport = await _sportService.CreateSport(request.Name);
    return Ok(_mapper.Map<SportResponse>(sport));
  }
 
  [HttpGet]
  public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken = new())
  {
    var cities = await _sportService.GetAllSports();
    return Ok(_mapper.Map<List<SportResponse>>(cities));
  }
  
  [HttpPatch]
  [Route("{id:int}")]
  public async Task<IActionResult> UpdateSport(int id, [FromBody] SportRequest request, CancellationToken cancellationToken = new())
  {
    var sport = await _sportService.UpdateSportName(id, request.Name);
    return Ok(_mapper.Map<SportResponse>(sport));
  }
  
  [HttpDelete]
  [Route("{id:int}")]
  public async Task<IActionResult> DeleteSport(int id, CancellationToken cancellationToken = new())
  {
    await _sportService.DeleteSport(id);
    return NoContent();
  }
  
}
