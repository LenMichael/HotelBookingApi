using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace HotelBookingApi.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var feedbacks = await _feedbackService.GetAllFeedback(cancellationToken);
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackService.GetFeedbackById(id, cancellationToken);
            if (feedback == null)
                return NotFound("Feedback not found.");
            return Ok(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Feedback feedback, CancellationToken cancellationToken)
        {
            var createdFeedback = await _feedbackService.CreateFeedback(feedback, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdFeedback.Id }, createdFeedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Feedback feedback, CancellationToken cancellationToken)
        {
            var updatedFeedback = await _feedbackService.UpdateFeedback(id, feedback, cancellationToken);
            if (updatedFeedback == null)
                return NotFound("Feedback not found.");
            //return Ok(updatedFeedback);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _feedbackService.DeleteFeedback(id, cancellationToken);
            if (!isDeleted)
                return NotFound("Feedback not found.");
            return NoContent();
        }
    }
}
