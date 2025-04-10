using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Models;
using Microsoft.AspNetCore.Authorization;
using HotelBookingApi.Services;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public ActionResult<IEnumerable<HotelBooking>> GetAll()
        {
            var bookings = _service.GetAllBookings();
            _logger.LogInformation("Retrieved all bookings.");
            return Ok(bookings);
        }

        [HttpGet]
        public ActionResult<HotelBooking> Get(int id)
        {
            var booking = _service.GetBookingById(id);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }
            return Ok(booking);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<HotelBooking> Create(HotelBooking booking)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking.");
                return BadRequest(ModelState);
            }

            _service.CreateBooking(booking);
            _logger.LogInformation("Created new booking.");
            return Ok(booking);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<HotelBooking> Edit(int id, HotelBooking booking)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking.");
                return BadRequest(ModelState);
            }
            // Check if the booking ID in the URL matches the booking ID in the body
            if (id != booking.Id)
            {
                _logger.LogWarning($"Booking ID mismatch: {id} != {booking.Id}");
                return BadRequest("Booking ID mismatch.");
            }
            // Check if the booking exists
            var existingBooking = _service.GetBookingById(id);
            if (existingBooking == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }

            _service.UpdateBooking(booking);
            _logger.LogInformation($"Updated booking with ID {id}.");
            return Ok(booking);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult<HotelBooking> Delete(int id)
        {
            var booking = _service.GetBookingById(id);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }

            _service.DeleteBooking(id);
            _logger.LogInformation($"Deleted booking with ID {id}.");
            return NoContent();
        }

        [HttpGet("paged")]
        public ActionResult<IEnumerable<HotelBooking>> GetAllPaged(int PageNumber = 1, int pageSize = 10)
        {
            var bookingsPaged = _service.GetAllBookings().Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
            _logger.LogInformation("Retrieved all bookings.");
            return Ok(bookingsPaged);
        }

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
