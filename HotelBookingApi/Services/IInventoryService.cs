using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllItems(CancellationToken cancellationToken);
        Task<Inventory?> GetItemById(int id, CancellationToken cancellationToken);
        Task<Inventory> CreateItem(Inventory item, CancellationToken cancellationToken);
        Task<Inventory?> UpdateItem(int id, Inventory item, CancellationToken cancellationToken);
        Task<bool> DeleteItem(int id, CancellationToken cancellationToken);
    }
}
