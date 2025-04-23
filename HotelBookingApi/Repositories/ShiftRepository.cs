using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ApiContext _context;

        public ShiftRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shift>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Shifts.ToListAsync(cancellationToken);
        }

        public async Task<Shift?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Shifts.FindAsync(id, cancellationToken);
        }

        public async Task<Shift> AddAsync(Shift shift, CancellationToken cancellationToken)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return shift;
        }

        public async Task<Shift> UpdateAsync(Shift shift, CancellationToken cancellationToken)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return shift;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.FindAsync(id, cancellationToken);
            if (shift == null) return false;

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
