using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelBookingApi.Repositories
{
    public class HotelBookingRepository : IHotelBookingRepository
    {
        private readonly ApiContext _context;

        public HotelBookingRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Bookings.FindAsync(id, cancellationToken);
        }

        public async Task Add(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Booking?> Update(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return booking;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(id, cancellationToken);
            if (booking != null) return false;
            
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
