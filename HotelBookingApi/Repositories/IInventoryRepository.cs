using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetByIdAsync(int id);
        Task<Inventory> AddAsync(Inventory item);
        Task<Inventory> UpdateAsync(Inventory item);
        Task<bool> DeleteAsync(int id);
    }
}
