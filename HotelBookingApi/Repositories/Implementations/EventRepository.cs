using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly ApiContext _context;

        public EventRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Events.ToListAsync(cancellationToken);
        }

        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Events.FindAsync( id, cancellationToken);
        }

        public async Task<Event> AddAsync(Event eventItem, CancellationToken cancellationToken)
        {
            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync(cancellationToken);
            return eventItem;
        }

        public async Task<Event?> UpdateAsync(int id, Event eventItem, CancellationToken cancellationToken)
        {
            var existingEvent = await _context.Events.FindAsync( id, cancellationToken);
            if (existingEvent == null) return null;

            _context.Entry(existingEvent).CurrentValues.SetValues(eventItem);
            await _context.SaveChangesAsync(cancellationToken);
            return existingEvent;
        }


        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var eventModel = await _context.Events.FindAsync(id);
            if (eventModel == null) return false;

            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
