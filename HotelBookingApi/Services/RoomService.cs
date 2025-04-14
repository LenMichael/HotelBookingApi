using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            return await _roomRepository.AddAsync(room);
        }

        public async Task<Room> UpdateRoomAsync(int id, Room room)
        {
            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null) return null;

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Type = room.Type;
            existingRoom.Price = room.Price;
            existingRoom.HotelId = room.HotelId;

            return await _roomRepository.UpdateAsync(existingRoom);
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            return await _roomRepository.DeleteAsync(id);
        }
    }
}
