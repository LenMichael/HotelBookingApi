using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> GetAllAsync();
        Task<Shift> GetByIdAsync(int id);
        Task<Shift> AddAsync(Shift shift);
        Task<Shift> UpdateAsync(Shift shift);
        Task<bool> DeleteAsync(int id);
    }
}
