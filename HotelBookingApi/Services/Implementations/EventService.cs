using FluentValidation;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<Event> _eventValidator;

        public EventService(IEventRepository eventRepository, IValidator<Event> eventValidator)
        {
            _eventRepository = eventRepository;
            _eventValidator = eventValidator;
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
            var validationResult = await _eventValidator.ValidateAsync(eventModel, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

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
