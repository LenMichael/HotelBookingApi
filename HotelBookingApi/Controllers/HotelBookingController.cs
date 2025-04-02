using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Models;
using HotelBookingApi.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        public HotelBookingController(ApiContext context, ILogger<HotelBookingController> logger, IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetAll()
        {
            if (!_cache.TryGetValue("bookings", out List<HotelBooking> bookings))
            {
                bookings = _context.GetBookings().ToList();
                _cache.Set("bookings", bookings);
            }
            return Ok(bookings);
        }


        //[HttpGet]
        //public ActionResult<IEnumerable<HotelBooking>> GetAll_Cache(int PageNumber = 1, int pageSize = 10)
        //{
        //    var cacheKey = $"GetAll_{PageNumber}_{pageSize}";
        //    if (!_cache.TryGetValue(cacheKey, out List<HotelBooking> bookings))
        //    {
        //        bookings = _context.GetBookings().Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
        //        var cacheEntryOptions = new MemoryCacheEntryOptions()
        //            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        //        _cache.Set(cacheKey, bookings, cacheEntryOptions);
        //        _logger.LogInformation("Retrieved bookings from database.");
        //    }
        //    else
        //        _logger.LogInformation("Retrieved bookings from cache.");

        //    return Ok(bookings);
        //}


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
            _cache.Set("bookings", _context.GetBookings().ToList());

            return Ok(booking);
        }

        [HttpGet]
        public ActionResult<HotelBooking> Get(int id)
        {
            if (!_cache.TryGetValue("bookings", out List<HotelBooking> bookings))
            {
                bookings = _context.GetBookings().ToList();
                _cache.Set("bookings", bookings);
            }

            var result = bookings?.FirstOrDefault(x => x.Id == id);
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
            if (!_cache.TryGetValue("bookings", out List<HotelBooking> bookings))
            {
                bookings = _context.GetBookings().ToList();
                _cache.Set("bookings", bookings);
            }

            var booking = bookings?.FirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID {id} not found.");
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            _logger.LogInformation($"Deleted booking with ID {id}.");

            bookings.Remove(booking);
            _cache.Set("bookings", bookings);

            return NoContent();
        }

        [HttpGet("paged")]
        public ActionResult<IEnumerable<HotelBooking>> GetAll(int PageNumber = 1, int pageSize = 10)
        {
            var result = _context.GetBookings().Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
            _logger.LogInformation("Retrieved all bookings.");
            return Ok(result);
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
