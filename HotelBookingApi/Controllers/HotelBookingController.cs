using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Models;
using Microsoft.AspNetCore.Authorization;
using HotelBookingApi.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly IHotelBookingService _service;
        private readonly ILogger<HotelBookingController> _logger;

        public HotelBookingController(IHotelBookingService service, ILogger<HotelBookingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll(CancellationToken cancellationToken)
        {
            var bookings = await _service.GetAllBookings(cancellationToken);
            _logger.LogInformation("Retrieved all bookings.");
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id, CancellationToken cancellationToken)
        {
            var booking = await _service.GetBookingById(id, cancellationToken);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }
            return Ok(booking);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Booking>> Create(Booking booking, CancellationToken cancellationToken)
        {
            try
            {
                var createdBooking = await _service.CreateBooking(booking, cancellationToken);
                _logger.LogInformation("Created new booking.");
                //return Ok(booking);
                return CreatedAtAction(nameof(GetById), new { id = createdBooking.Id }, createdBooking);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    Message = "Validation failed.",
                    Errors = new [] { ex.Message }
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Booking>> Update(int id, Booking booking, CancellationToken cancellationToken)
        {
            if (id != booking.Id)
            {
                _logger.LogWarning($"Booking ID mismatch: {id} != {booking.Id}");
                return BadRequest("Booking ID mismatch.");
            }

            var updatedBooking = await _service.UpdateBooking(booking, cancellationToken);
            if (updatedBooking == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Updated booking with ID {id}.");
            //return Ok(updatedBooking);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _service.DeleteBooking(id, cancellationToken);
            if (!isDeleted)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Deleted booking with ID {id}.");
            return NoContent();
        }

        //[HttpGet("paged")]
        //public ActionResult<IEnumerable<Booking>> GetAllPaged(int PageNumber = 1, int pageSize = 10)
        //{
        //    var bookingsPaged = _service.GetAllBookings().Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
        //    _logger.LogInformation("Retrieved all bookings.");
        //    return Ok(bookingsPaged);
        //}

        #region Filter Methods
        //[HttpGet]
        //public ActionResult<IEnumerable<HotelBooking>> GetByRoomNumber(int roomNumber)
        //{
        //    var result = _context.GetBookings().Where(x => x.RoomNumber == roomNumber).ToList();
        //    return Ok(result);
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<HotelBooking>> GetByClientName(string clientName)
        //{
        //    var result = _context.GetBookings().Where(x => x.ClientName == clientName).ToList();
        //    return Ok(result);
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<HotelBooking>> GetByRoomNumberAndClientName(int roomNumber, string clientName)
        //{
        //    var result = _context.GetBookings().Where(x => x.RoomNumber == roomNumber && x.ClientName == clientName).ToList();
        //    return Ok(result);
        //}
        //[HttpGet]
        //public ActionResult<IEnumerable<HotelBooking>> GetByRoomNumberOrClientName(int roomNumber, string clientName)
        //{
        //    var result = _context.GetBookings().Where(x => x.RoomNumber == roomNumber || x.ClientName == clientName).ToList();
        //    return Ok(result);
        //}
        //[HttpGet]
        //public ActionResult<IEnumerable<HotelBooking>> GetFirstBookings()
        //{
        //    var result = _context.GetBookings().Take(3).ToList();
        //    return Ok(result);
        //}
        #endregion
    }
}
