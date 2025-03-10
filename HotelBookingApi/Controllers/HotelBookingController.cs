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
        public JsonResult CreateEdit(HotelBooking booking)
        {
            if (booking.Id == 0)
            {
                _context.Bokkings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bokkings.Find(booking.Id);
                if (bookingInDb == null)
                    return new JsonResult(NotFound());

                bookingInDb = booking;
            }
            
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bokkings.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bokkings.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            _context.Bokkings.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Bokkings.ToList();
            return new JsonResult(result);
        }
    }
}
