using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApiContext _context;

        public EventRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> AddAsync(Event eventModel)
        {
            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<Event> UpdateAsync(Event eventModel)
        {
            _context.Events.Update(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var eventModel = await _context.Events.FindAsync(id);
            if (eventModel == null) return false;

            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
