using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms(CancellationToken cancellationToken);
        Task<Room?> GetRoomById(int id, CancellationToken cancellationToken);
        Task<Room> CreateRoom(Room room, CancellationToken cancellationToken);
        Task<Room?> UpdateRoom(int id, Room room, CancellationToken cancellationToken);
        Task<bool> DeleteRoom(int id, CancellationToken cancellationToken);
    }
}
