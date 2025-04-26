using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Inventory>> GetAllItems(CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }

        public async Task<Inventory?> GetItemById(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetById(id, cancellationToken);
        }

        public async Task<Inventory> CreateItem(Inventory item, CancellationToken cancellationToken)
        {
            return await _repository.Add(item, cancellationToken);
        }

        public async Task<Inventory?> UpdateItem(int id, Inventory item, CancellationToken cancellationToken)
        {
            var existingItem = await _repository.GetById(id, cancellationToken);
            if (existingItem == null) return null;

            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            existingItem.LastUpdated = DateTime.UtcNow;

            return await _repository.Update(existingItem, cancellationToken);
        }

        public async Task<bool> DeleteItem(int id, CancellationToken cancellationToken)
        {
            return await _repository.Delete(id, cancellationToken);
        }
    }
}
