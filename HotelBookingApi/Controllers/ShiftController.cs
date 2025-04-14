using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShifts()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftById(int id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);
            if (shift == null)
                return NotFound("Shift not found.");
            return Ok(shift);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] Shift shift)
        {
            var createdShift = await _shiftService.CreateShiftAsync(shift);
            return CreatedAtAction(nameof(GetShiftById), new { id = createdShift.Id }, createdShift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift shift)
        {
            var updatedShift = await _shiftService.UpdateShiftAsync(id, shift);
            if (updatedShift == null)
                return NotFound("Shift not found.");
            return Ok(updatedShift);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var isDeleted = await _shiftService.DeleteShiftAsync(id);
            if (!isDeleted)
                return NotFound("Shift not found.");
            return NoContent();
        }
    }
}

