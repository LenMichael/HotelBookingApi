using HotelBookingApi.Models;

namespace HotelBookingApi.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync(CancellationToken cancellationToken);
        Task<Event?> GetEventByIdAsync(int id, CancellationToken cancellationToken);
        Task<Event> CreateEventAsync(Event eventModel, CancellationToken cancellationToken);
        Task<Event?> UpdateEventAsync(int id, Event eventModel, CancellationToken cancellationToken);
        Task<bool> DeleteEventAsync(int id, CancellationToken cancellationToken);
    }
}
