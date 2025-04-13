using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IHotelBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(int id);  //GetBookingById
        void Add(Booking booking);  //CreateBooking
        void Update(Booking booking);  //UpdateBooking
        void Delete(int id);  //DeleteBooking
    }
}
