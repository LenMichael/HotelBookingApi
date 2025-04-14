using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task<Event> AddAsync(Event eventModel);
        Task<Event> UpdateAsync(Event eventModel);
        Task<bool> DeleteAsync(int id);
    }
}
