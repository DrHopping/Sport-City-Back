using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Services;
using SportCity.Web.Attributes;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public PlayersController(IMapper mapper, IPlayerService playerService)
    {
        _mapper = mapper;
        _playerService = playerService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPlayers(CancellationToken cancellationToken = new())
    {
        var players = await _playerService.GetAllPlayers();
        return Ok(_mapper.Map<List<PlayerResponse>>(players));
    }

    [HttpPut]
    [Route("{id:int}")]
    [OwningUserAccess(typeof(Player))]
    public async Task<IActionResult> UpdatePlayer([OwningUserAccessId] int id, [FromBody] PlayerRequest request)
    {
        var playerUpdates = _mapper.Map<Player>(request);
        await _playerService.UpdatePlayer(id, playerUpdates);
        return NoContent();
    }
}
