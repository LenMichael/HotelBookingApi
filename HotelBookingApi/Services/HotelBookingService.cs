using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class HotelBookingService : IHotelBookingService
    {
        private readonly IHotelBookingRepository _repository;

        public HotelBookingService(IHotelBookingRepository repository)
        {
            _repository = repository;
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
            await _repository.Add(booking, cancellationToken);
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
