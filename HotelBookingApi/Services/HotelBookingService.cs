using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace HotelBookingApi.Services
{
    public class HotelBookingService : IHotelBookingService
    {
        private readonly IHotelBookingRepository _repository;
        private readonly IMemoryCache _cache;

        public HotelBookingService(IHotelBookingRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public IEnumerable<HotelBooking> GetAllBookings()
        {
            if (!_cache.TryGetValue("bookings", out IEnumerable<HotelBooking> bookings))
            {
                bookings = _repository.GetAll().ToList();
                _cache.Set("bookings", bookings);
            }
            return bookings;
        }

        public HotelBooking GetBookingById(int id)
        {
            var bookings = GetAllBookings();
            return bookings.FirstOrDefault(x=> x.Id == id);
        }

        public void CreateBooking(HotelBooking booking)
        {
            _repository.Add(booking);
            RefreshCache();
        }

        public void UpdateBooking(HotelBooking booking)
        {
            _repository.Update(booking);
            RefreshCache();
        }

        public void DeleteBooking(int id)
        {
            _repository.Delete(id);
            RefreshCache();
        }

        private void RefreshCache()
        {
            var bookings = _repository.GetAll().ToList();
            _cache.Set("bookings", bookings);
        }
    }
}
