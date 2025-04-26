using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAll(CancellationToken cancellationToken);
        Task<Inventory?> GetById(int id, CancellationToken cancellationToken);
        Task<Inventory> Add(Inventory item, CancellationToken cancellationToken);
        Task<Inventory> Update(Inventory item, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
