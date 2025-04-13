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

        public IEnumerable<Booking> GetAllBookings()
        {
            return _repository.GetAll();
        }

        public Booking GetBookingById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateBooking(Booking booking)
        {
            _repository.Add(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            _repository.Update(booking);
        }

        public void DeleteBooking(int id)
        {
            _repository.Delete(id);
        }
    }
}
