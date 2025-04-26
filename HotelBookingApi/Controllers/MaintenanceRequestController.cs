using HotelBookingApi.Models;
using HotelBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Controllers
{
    [Route("api/maintenance-requests")]
    [ApiController]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly IMaintenanceRequestService _maintenanceRequestService;

        public MaintenanceRequestController(IMaintenanceRequestService maintenanceRequestService)
        {
            _maintenanceRequestService = maintenanceRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var requests = await _maintenanceRequestService.GetAllRequests(cancellationToken);
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = await _maintenanceRequestService.GetRequestById(id, cancellationToken);
            if (request == null)
                return NotFound("Maintenance request not found.");
            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaintenanceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createdRequest = await _maintenanceRequestService.CreateRequest(request, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = createdRequest.Id }, createdRequest);

            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    Message = "Validation failed.",
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MaintenanceRequest request, CancellationToken cancellationToken)
        {
            var updatedRequest = await _maintenanceRequestService.UpdateRequest(id, request, cancellationToken);
            if (updatedRequest == null)
                return NotFound("Maintenance request not found.");
            //return Ok(updatedRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _maintenanceRequestService.DeleteRequest(id, cancellationToken);
            if (!isDeleted)
                return NotFound("Maintenance request not found.");
            return NoContent();
        }
    }
}

