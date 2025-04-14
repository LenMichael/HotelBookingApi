using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApiContext _context;

        public InventoryRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }

        public async Task<Inventory> AddAsync(Inventory item)
        {
            _context.Inventories.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Inventory> UpdateAsync(Inventory item)
        {
            _context.Inventories.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Inventories.FindAsync(id);
            if (item == null) return false;

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
