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

        public async Task<IEnumerable<Room>> GetAllRooms(CancellationToken cancellationToken)
        {
            return await _roomRepository.GetAll(cancellationToken);
        }

        public async Task<Room?> GetRoomById(int id, CancellationToken cancellationToken)
        {
            return await _roomRepository.GetById(id, cancellationToken);
        }

        public async Task<Room> CreateRoom(Room room, CancellationToken cancellationToken)
        {
            return await _roomRepository.Add(room, cancellationToken);
        }

        public async Task<Room?> UpdateRoom(int id, Room room, CancellationToken cancellationToken)
        {
            var existingRoom = await _roomRepository.GetById(id, cancellationToken);
            if (existingRoom == null) return null;

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Type = room.Type;
            existingRoom.Price = room.Price;
            existingRoom.HotelId = room.HotelId;

            return await _roomRepository.Update(existingRoom, cancellationToken);
        }

        public async Task<bool> DeleteRoom(int id, CancellationToken cancellationToken)
        {
            return await _roomRepository.Delete(id, cancellationToken);
        }
    }
}
