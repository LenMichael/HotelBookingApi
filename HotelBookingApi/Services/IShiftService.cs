using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> GetAllShiftsAsync();
        Task<Shift> GetShiftByIdAsync(int id);
        Task<Shift> CreateShiftAsync(Shift shift);
        Task<Shift> UpdateShiftAsync(int id, Shift shift);
        Task<bool> DeleteShiftAsync(int id);
    }
}
