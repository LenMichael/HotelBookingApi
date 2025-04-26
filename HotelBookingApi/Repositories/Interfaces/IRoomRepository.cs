using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAll(CancellationToken cancellationToken);
        Task<Room?> GetById(int id, CancellationToken cancellationToken);
        Task<Room> Add(Room room, CancellationToken cancellationToken);
        Task<Room> Update(Room room, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}   
