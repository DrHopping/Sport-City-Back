using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
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
  public async Task<IActionResult> GetAllPlayers(CancellationToken cancellationToken = new())
  {
    var players = await _playerService.GetAllPlayers();
    return Ok(_mapper.Map<List<PlayerResponse>>(players));
  }
}
