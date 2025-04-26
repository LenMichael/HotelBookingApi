using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApiContext _context;

        public RoomRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Rooms.ToListAsync(cancellationToken);
        }

        public async Task<Room?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> Add(Room room, CancellationToken cancellationToken)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync(cancellationToken);
            return room;
        }

        public async Task<Room> Update(Room room, CancellationToken cancellationToken)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync(cancellationToken);
            return room;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FindAsync(id, cancellationToken);
            if (room == null) return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
