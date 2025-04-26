using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApiContext _context;

        public InventoryRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Inventories.ToListAsync(cancellationToken);
        }

        public async Task<Inventory?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Inventories.FindAsync(id, cancellationToken);
        }

        public async Task<Inventory> Add(Inventory item, CancellationToken cancellationToken)
        {
            _context.Inventories.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task<Inventory> Update(Inventory item, CancellationToken cancellationToken)
        {
            _context.Inventories.Update(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var item = await _context.Inventories.FindAsync(id, cancellationToken);
            if (item == null) return false;

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
