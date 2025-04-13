using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IHotelBookingService
    {
        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        void CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int id);
    }
}
