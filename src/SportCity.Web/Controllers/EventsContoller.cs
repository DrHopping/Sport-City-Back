using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.SharedKernel;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventsController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
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
}
