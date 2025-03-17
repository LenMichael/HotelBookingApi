using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Models;
using HotelBookingApi.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ILogger<HotelBookingController> _logger;

        public HotelBookingController(ApiContext context, ILogger<HotelBookingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<HotelBooking> CreateEdit(HotelBooking booking)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking.");
                return BadRequest(ModelState);
            }

            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
                _logger.LogInformation("Added new booking.");
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);
                if (bookingInDb == null)
                {
                    _logger.LogWarning($"Booking with ID {booking.Id} not found.");
                    return NotFound();
                }

                bookingInDb = booking;
                _logger.LogInformation($"Updated booking with ID {booking.Id}.");
            }

            _context.SaveChanges();
            return Ok(booking);
        }

        [HttpGet]
        public ActionResult<HotelBooking> Get(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<HotelBooking> Delete(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }

            _context.Bookings.Remove(result);
            _context.SaveChanges();
            _logger.LogInformation($"Deleted booking with ID {id}.");
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetAll(int PageNumber = 1, int pageSize = 10)
        {
            var result = _context.GetBookings().Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
            _logger.LogInformation("Retrieved all bookings.");
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetByRoomNumber(int roomNumber)
        {
            var result = _context.GetBookings().Where(x => x.RoomNumber == roomNumber).ToList();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetByClientName(string clientName)
        {
            var result = _context.GetBookings().Where(x => x.ClientName == clientName).ToList();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetByRoomNumberAndClientName(int roomNumber, string clientName)
        {
            var result = _context.GetBookings().Where(x => x.RoomNumber == roomNumber && x.ClientName == clientName).ToList();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetByRoomNumberOrClientName(int roomNumber, string clientName)
        {
            var result = _context.GetBookings().Where(x => x.RoomNumber == roomNumber || x.ClientName == clientName).ToList();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetFirstBookings()
        {
            var result = _context.GetBookings().Take(3).ToList();
            return Ok(result);
        }
    }
}
