using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IHotelBookingService
    {
        IEnumerable<HotelBooking> GetAllBookings();
        HotelBooking GetBookingById(int id);
        void CreateBooking(HotelBooking booking);
        void UpdateBooking(HotelBooking booking);
        void DeleteBooking(int id);
    }
}
