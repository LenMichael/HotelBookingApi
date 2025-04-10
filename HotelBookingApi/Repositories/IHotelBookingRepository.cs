using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IHotelBookingRepository
    {
        IEnumerable<HotelBooking> GetAll();
        HotelBooking GetById(int id);  //GetBookingById
        void Add(HotelBooking booking);  //CreateBooking
        void Update(HotelBooking booking);  //UpdateBooking
        void Delete(int id);  //DeleteBooking
    }
}
