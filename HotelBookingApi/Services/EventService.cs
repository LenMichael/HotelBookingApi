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

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task<Event> CreateEventAsync(Event eventModel)
        {
            return await _eventRepository.AddAsync(eventModel);
        }

        public async Task<Event> UpdateEventAsync(int id, Event eventModel)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(id);
            if (existingEvent == null) return null;

            existingEvent.Name = eventModel.Name;
            existingEvent.Date = eventModel.Date;
            existingEvent.Location = eventModel.Location;
            existingEvent.Organizer = eventModel.Organizer;
            existingEvent.Description = eventModel.Description;

            return await _eventRepository.UpdateAsync(existingEvent);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            return await _eventRepository.DeleteAsync(id);
        }
    }
}
