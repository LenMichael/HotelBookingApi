using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllItemsAsync();
        Task<Inventory> GetItemByIdAsync(int id);
        Task<Inventory> CreateItemAsync(Inventory item);
        Task<Inventory> UpdateItemAsync(int id, Inventory item);
        Task<bool> DeleteItemAsync(int id);
    }
}
