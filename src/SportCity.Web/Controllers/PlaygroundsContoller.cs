using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.SharedKernel;
using SportCity.Web.Attributes;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaygroundsController : ControllerBase
{
    private readonly IPlaygroundService _playgroundService;
    private readonly IMapper _mapper;

    public PlaygroundsController(IPlaygroundService playgroundService, IMapper mapper)
    {
        _playgroundService = playgroundService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreatePlayground([FromBody] PlaygroundRequest request,
        CancellationToken cancellationToken = new())
    {
        var playground = await _playgroundService.CreatePlayground
        (
            request.Name,
            request.Description,
            request.CityId,
            request.PhotoUrl,
            request.Location
        );
        return Ok(_mapper.Map<PlaygroundCreateResponse>(playground));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlaygrounds(CancellationToken cancellationToken = new())
    {
        var playgrounds = await _playgroundService.GetAllPlaygrounds();
        return Ok(_mapper.Map<List<PlaygroundListResponse>>(playgrounds));
    }

    [HttpGet]
    [Route("/api/cities/{cityId:int}/playgrounds")]
    public async Task<IActionResult> GetCityPlaygrounds(int cityId,
        CancellationToken cancellationToken = new())
    {
        var playgrounds = await _playgroundService.GetCityPlaygrounds(cityId);
        return Ok(_mapper.Map<List<PlaygroundListResponse>>(playgrounds));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetPlayground(int id, CancellationToken cancellationToken = new())
    {
        var playgrounds = await _playgroundService.GetPlaygroundById(id);
        return Ok(_mapper.Map<PlaygroundResponse>(playgrounds));
    }


    /*[HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdatePlayground(int id, [FromBody] SportRequest request, CancellationToken cancellationToken = new())
    {
      var sport = await _playgroundService.UpdatePlayground(id, );
      return Ok(_mapper.Map<SportResponse>(sport));
    }*/

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeletePlayground(int id, CancellationToken cancellationToken = new())
    {
        await _playgroundService.DeletePlayground(id);
        return NoContent();
    }
}
