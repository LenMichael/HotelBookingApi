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

        public IEnumerable<HotelBooking> GetAllBookings()
        {
            return _repository.GetAll();
        }

        public HotelBooking GetBookingById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateBooking(HotelBooking booking)
        {
            _repository.Add(booking);
        }

        public void UpdateBooking(HotelBooking booking)
        {
            _repository.Update(booking);
        }

        public void DeleteBooking(int id)
        {
            _repository.Delete(id);
        }
    }
}
