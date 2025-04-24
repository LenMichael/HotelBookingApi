using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        // GET: api/shifts
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var shifts = await _shiftService.GetAllShiftsAsync(cancellationToken);
            return Ok(shifts);
        }

        // GET: api/shifts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id, cancellationToken);
            if (shift == null)
                return NotFound("Shift not found.");
            return Ok(shift);
        }

        // POST: api/shifts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Shift shift, CancellationToken cancellationToken)
        {
            var createdShift = await _shiftService.CreateShiftAsync(shift, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdShift.Id }, createdShift);
        }

        // PUT: api/shifts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Shift shift, CancellationToken cancellationToken)
        {
            var updatedShift = await _shiftService.UpdateShiftAsync(id, shift, cancellationToken);
            if (updatedShift == null)
                return NotFound("Shift not found.");
            //return Ok(updatedShift);
            return NoContent();
        }

        // DELETE: api/shifts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _shiftService.DeleteShiftAsync(id, cancellationToken);
            if (!isDeleted)
                return NotFound("Shift not found.");
            return NoContent();
        }
    }
}

