using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IHotelBookingRepository
    {
        Task<IEnumerable<Booking>> GetAll(CancellationToken cancellationToken);
        Task<Booking?> GetById(int id, CancellationToken cancellationToken);  //GetBookingById
        Task<Booking> Add(Booking booking, CancellationToken cancellationToken);  //CreateBooking
        Task<Booking?> Update(Booking booking, CancellationToken cancellationToken);  //UpdateBooking
        Task<bool> Delete(int id, CancellationToken cancellationToken);  //DeleteBooking
    }
}
