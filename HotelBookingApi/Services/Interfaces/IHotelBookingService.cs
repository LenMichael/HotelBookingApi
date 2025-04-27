using HotelBookingApi.Models;

namespace HotelBookingApi.Services.Interfaces
{
    public interface IHotelBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookings(CancellationToken cancellationToken);
        Task<Booking?> GetBookingById(int id, CancellationToken cancellationToken);
        Task/*<Booking>*/ CreateBooking(Booking booking, CancellationToken cancellationToken);
        Task<Booking?> UpdateBooking(Booking booking, CancellationToken cancellationToken);
        Task<bool> DeleteBooking(int id, CancellationToken cancellationToken);
    }
}
