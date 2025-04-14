using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Inventory>> GetAllItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Inventory> GetItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Inventory> CreateItemAsync(Inventory item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task<Inventory> UpdateItemAsync(int id, Inventory item)
        {
            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null) return null;

            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            existingItem.LastUpdated = DateTime.UtcNow;

            return await _repository.UpdateAsync(existingItem);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
