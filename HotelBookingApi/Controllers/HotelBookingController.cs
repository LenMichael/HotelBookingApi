using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Models;
using HotelBookingApi.Data;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {

        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }


        [HttpPost]
        //public JsonResult CreateEdit(HotelBooking booking)
        public ActionResult<HotelBooking> CreateEdit(HotelBooking booking)
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);
                if (bookingInDb == null)
                    //return new JsonResult(NotFound());
                    return NotFound();

                bookingInDb = booking;
            }
            
            _context.SaveChanges();
            //return new JsonResult(Ok(booking));
            return Ok(booking);
        }

        [HttpGet]
        //public JsonResult Get(int id)
        public ActionResult<HotelBooking> Get(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
                //return new JsonResult(NotFound());
                return NotFound();

            //return new JsonResult(Ok(result));
            return Ok(result);
        }

        [HttpDelete]
        //public JsonResult Delete(int id)
        public ActionResult<HotelBooking> Delete(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
                //return new JsonResult(NotFound());
                return NotFound();

            _context.Bookings.Remove(result);
            _context.SaveChanges();
            //return new JsonResult(NoContent());
            return NoContent();
        }

        [HttpGet]
        //public JsonResult GetAll()
        public ActionResult<HotelBooking> GetAll()
        {
            var result = _context.Bookings.ToList();
            //return new JsonResult(result);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<HotelBooking> GetByRoomNumber(int roomNumber)
        {
            var result = _context.Bookings.Where(x => x.RoomNumber == roomNumber).ToList();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<HotelBooking> GetByClientName(string clientName)
        {
            var result = _context.Bookings.Where(x => x.ClientName == clientName).ToList();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<HotelBooking> GetByRoomNumberAndClientName(int roomNumber, string clientName)
        {
            var result = _context.Bookings.Where(x => x.RoomNumber == roomNumber && x.ClientName == clientName).ToList();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<HotelBooking> GetByRoomNumberOrClientName(int roomNumber, string clientName)
        {
            var result = _context.Bookings.Where(x => x.RoomNumber == roomNumber || x.ClientName == clientName).ToList();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<HotelBooking> GetFirstBookings()
        {
            var result = _context.Bookings.Take(3).ToList();
            return Ok(result);
        }


    }
}
