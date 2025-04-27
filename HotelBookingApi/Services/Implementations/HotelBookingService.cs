using FluentValidation;
using Hangfire;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class HotelBookingService : IHotelBookingService
    {
        private readonly IHotelBookingRepository _repository;
        private readonly IValidator<Booking> _bookingValidator;
        private readonly IEmailService _emailService;

        public HotelBookingService(IHotelBookingRepository repository, IValidator<Booking> bookingValidator, IEmailService emailService)
        {
            _repository = repository;
            _bookingValidator = bookingValidator;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings(CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }

        public async Task<Booking?> GetBookingById(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetById(id, cancellationToken);
        }

        public async Task CreateBooking(Booking booking, CancellationToken cancellationToken)
        {
            var validationResult = await _bookingValidator.ValidateAsync(booking, cancellationToken);
            
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            await _repository.Add(booking, cancellationToken);
            BackgroundJob.Enqueue(() => _emailService.SendBookingEmail(booking.Id, booking.User.Email));
        }

        public async Task<Booking?> UpdateBooking(Booking booking, CancellationToken cancellationToken)
        {
            return await _repository.Update(booking, cancellationToken);
        }

        public async Task<bool> DeleteBooking(int id, CancellationToken cancellationToken)
        {
            return await _repository.Delete(id, cancellationToken);
        }
    }
}
