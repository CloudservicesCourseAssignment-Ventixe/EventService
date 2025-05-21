using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventservice eventService) : ControllerBase
{
    private readonly IEventservice _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await _eventService.GetAllEventsAsync();
        var events = result.Result;
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneEvent(string id)
    {
        var result = await _eventService.GetOneEventAsync(id);
        if (result.Succeeded)
        {
            var currentEvent = result.Result;
            return Ok(currentEvent);
        }
        return NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _eventService.CreateEvent(request);
        return result.Succeeded ? Ok() : StatusCode(500, result.Error);
    }
}
