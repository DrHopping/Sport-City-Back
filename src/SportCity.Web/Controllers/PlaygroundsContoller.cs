﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Services;
using SportCity.SharedKernel;
using SportCity.Web.Models;
using IAuthorizationService = SportCity.Core.Interfaces.IAuthorizationService;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaygroundsController : ControllerBase
{
    private readonly IPlaygroundService _playgroundService;
    private readonly IAuthorizationService _authService;
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public PlaygroundsController(
        IPlaygroundService playgroundService,
        IMapper mapper,
        IAuthorizationService authService,
        IPlayerService playerService)
    {
        _playgroundService = playgroundService;
        _mapper = mapper;
        _authService = authService;
        _playerService = playerService;
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

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdatePlayground(int id, [FromBody] PlaygroundRequest request,
        CancellationToken cancellationToken = new())
    {
        var playground = _mapper.Map<Playground>(request);
        var updatedPlayground = await _playgroundService.UpdatePlayground(id, playground);
        return Ok(_mapper.Map<PlaygroundResponse>(updatedPlayground));
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeletePlayground(int id, CancellationToken cancellationToken = new())
    {
        await _playgroundService.DeletePlayground(id);
        return NoContent();
    }

    [HttpPost]
    [Route("{playgroundId:int}/reviews")]
    [Authorize]
    public async Task<IActionResult> AddReview(int playgroundId, [FromBody] AddReviewRequest request,
        CancellationToken cancellationToken = new())
    {
        var userId = _authService.GetIdentity();
        var player = await _playerService.GetPlayerById(userId);

        var result = await _playgroundService.AddReview(playgroundId, player.Id, request.Rating, request.Comment);
        return Ok(result);
    }


}
