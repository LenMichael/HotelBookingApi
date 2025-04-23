using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken);
        Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Event> AddAsync(Event eventModel, CancellationToken cancellationToken);
        Task<Event?> UpdateAsync(int id, Event eventModel, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
