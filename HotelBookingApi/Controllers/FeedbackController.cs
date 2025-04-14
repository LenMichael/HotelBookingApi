using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            var feedbacks = await _feedbackService.GetAllFeedbackAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
                return NotFound("Feedback not found.");
            return Ok(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] Feedback feedback)
        {
            var createdFeedback = await _feedbackService.CreateFeedbackAsync(feedback);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.Id }, createdFeedback);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateFeedback(int id, [FromBody] Feedback feedback)
        //{
        //    var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(id, feedback);
        //    if (updatedFeedback == null)
        //        return NotFound("Feedback not found.");
        //    return Ok(updatedFeedback);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var isDeleted = await _feedbackService.DeleteFeedbackAsync(id);
            if (!isDeleted)
                return NotFound("Feedback not found.");
            return NoContent();
        }
    }
}
