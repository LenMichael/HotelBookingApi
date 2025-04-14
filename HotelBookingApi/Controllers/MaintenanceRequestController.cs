using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly IMaintenanceRequestService _maintenanceRequestService;

        public MaintenanceRequestController(IMaintenanceRequestService maintenanceRequestService)
        {
            _maintenanceRequestService = maintenanceRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _maintenanceRequestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var request = await _maintenanceRequestService.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound("Maintenance request not found.");
            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] MaintenanceRequest request)
        {
            var createdRequest = await _maintenanceRequestService.CreateRequestAsync(request);
            return CreatedAtAction(nameof(GetRequestById), new { id = createdRequest.Id }, createdRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] MaintenanceRequest request)
        {
            var updatedRequest = await _maintenanceRequestService.UpdateRequestAsync(id, request);
            if (updatedRequest == null)
                return NotFound("Maintenance request not found.");
            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var isDeleted = await _maintenanceRequestService.DeleteRequestAsync(id);
            if (!isDeleted)
                return NotFound("Maintenance request not found.");
            return NoContent();
        }
    }
}

