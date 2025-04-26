using HotelBookingApi.Models;
using HotelBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var events = await _eventService.GetAllEventsAsync(cancellationToken);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id, cancellationToken);
            if (eventItem == null)
                return NotFound("Event not found.");
            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event eventItem, CancellationToken cancellationToken)
        {
            var createdEvent = await _eventService.CreateEventAsync(eventItem, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event eventItem, CancellationToken cancellationToken)
        {
            var updatedEvent = await _eventService.UpdateEventAsync(id, eventItem, cancellationToken);
            if (updatedEvent == null)
                return NotFound("Event not found.");
            //return Ok(updatedEvent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _eventService.DeleteEventAsync(id, cancellationToken);
            if (!isDeleted)
                return NotFound("Event not found.");
            return NoContent();
        }
    }
}
