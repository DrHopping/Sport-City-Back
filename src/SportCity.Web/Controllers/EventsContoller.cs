﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Services;
using SportCity.SharedKernel;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAllEvents(CancellationToken cancellationToken = new())
    {
        var events = await _eventService.GetAllEvents();
        return Ok(_mapper.Map<List<EventListResponse>>(events));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("{id:int}")]
    public async Task<IActionResult> GetEvent(int id, CancellationToken cancellationToken = new())
    {
        var @event = await _eventService.GetEvent(id);
        return Ok(_mapper.Map<EventResponse>(@event));
    }

}
