using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.Web.Models;
using IAuthorizationService = SportCity.Core.Interfaces.IAuthorizationService;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IPlayerService _playerService;
    private readonly IAuthorizationService _authService;
    private readonly IMapper _mapper;

    public EventsController(
        IEventService eventService,
        IMapper mapper,
        IPlayerService playerService,
        IAuthorizationService authService)
    {
        _eventService = eventService;
        _mapper = mapper;
        _playerService = playerService;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents(CancellationToken cancellationToken = new())
    {
        var events = await _eventService.GetAllEvents();
        return Ok(_mapper.Map<List<EventListResponse>>(events));
    }

    [HttpGet]
    [Route("/api/cities/{cityId:int}/events")]
    public async Task<IActionResult> GetCityEvents(int cityId, CancellationToken cancellationToken = new())
    {
        var events = await _eventService.GetCityEvents(cityId);
        return Ok(_mapper.Map<List<EventListResponse>>(events));
    }

    [HttpGet]
    [Route("/api/playgrounds/{playgroundId:int}/events")]
    public async Task<IActionResult> GetPlaygroundEvents(int playgroundId, CancellationToken cancellationToken = new())
    {
        var events = await _eventService.GetPlaygroundEvents(playgroundId);
        return Ok(_mapper.Map<List<EventListResponse>>(events));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetEvent(int id, CancellationToken cancellationToken = new())
    {
        var @event = await _eventService.GetEvent(id);
        return Ok(_mapper.Map<EventResponse>(@event));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateEvent(CreateEventRequest request, CancellationToken cancellationToken = new())
    {
        var userId = _authService.GetIdentity();
        var organizer = await _playerService.GetPlayerById(userId);
        var @event = await _eventService.CreateEvent(request.CategoryId, request.SportId, organizer.Id,
            request.PlaygroundId, request.Capacity, request.DateTime);
        return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, _mapper.Map<EventResponse>(@event));
    }

    [HttpPut]
    [Authorize]
    [Route("{eventId:int}/participants")]
    public async Task<IActionResult> AddParticipant(int eventId, CancellationToken cancellationToken = new())
    {
        var userId = _authService.GetIdentity();
        var player = await _playerService.GetPlayerById(userId);
        var @event = await _eventService.AddParticipant(eventId, player.Id);
        return Ok(_mapper.Map<EventResponse>(@event));
    }

    [HttpDelete]
    [Route("{eventId:int}/participants")]
    [Authorize]
    public async Task<IActionResult> RemoveParticipant(int eventId, CancellationToken cancellationToken = new())
    {
        var userId = _authService.GetIdentity();
        var player = await _playerService.GetPlayerById(userId);
        var @event = await _eventService.RemoveParticipant(eventId, player.Id);
        return Ok(_mapper.Map<EventResponse>(@event));
    }
}
