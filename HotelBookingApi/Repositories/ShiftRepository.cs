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

        public async Task<IEnumerable<Shift>> GetAllAsync()
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task<Shift> GetByIdAsync(int id)
        {
            return await _context.Shifts.FindAsync(id);
        }

        public async Task<Shift> AddAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<Shift> UpdateAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return false;

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
