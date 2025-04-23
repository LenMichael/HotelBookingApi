using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(CancellationToken cancellationToken)
        {
            return await _eventRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Event?> GetEventByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Event> CreateEventAsync(Event eventModel, CancellationToken cancellationToken)
        {
            return await _eventRepository.AddAsync(eventModel, cancellationToken);
        }

        public async Task<Event?> UpdateEventAsync(int id, Event eventModel, CancellationToken cancellationToken)
        {
            return await _eventRepository.UpdateAsync(id, eventModel, cancellationToken);
        }

        public async Task<bool> DeleteEventAsync(int id, CancellationToken cancellationToken)
        {
            return await _eventRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
