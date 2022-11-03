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
[Authorize(Roles = Roles.Admin)]
public class PlaygroundController : ControllerBase
{
    private readonly IPlaygroundService _playgroundService;
    private readonly IMapper _mapper;

    public PlaygroundController(IPlaygroundService playgroundService, IMapper mapper)
    {
        _playgroundService = playgroundService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlayground([FromBody] PlaygroundRequest request,
        CancellationToken cancellationToken = new())
    {
        var playground =
            await _playgroundService.CreatePlayground(request.Name, request.Description, request.CityId,
                request.Location);
        return Ok(_mapper.Map<PlaygroundCreateResponse>(playground));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPlaygrounds(CancellationToken cancellationToken = new())
    {
        var playgrounds = await _playgroundService.GetAllPlaygrounds();
        return Ok(_mapper.Map<List<PlaygroundGetResponse>>(playgrounds));
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
    public async Task<IActionResult> DeletePlayground(int id, CancellationToken cancellationToken = new())
    {
        await _playgroundService.DeletePlayground(id);
        return NoContent();
    }
}
