using HotelBookingApi.Data;
using HotelBookingApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingApi.Repositories
{
    public class HotelBookingRepository : IHotelBookingRepository
    {
        private readonly ApiContext _context;

        public HotelBookingRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelBooking> GetAll()
        {
            return _context.GetBookings().ToList();
        }

        public HotelBooking GetById(int id)
        {
            return _context.Bookings.Find(id);
        }

        public void Add(HotelBooking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void Update(HotelBooking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }
    }
}
