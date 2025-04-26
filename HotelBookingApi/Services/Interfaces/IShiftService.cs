using HotelBookingApi.Models;

namespace HotelBookingApi.Services.Interfaces
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> GetAllShiftsAsync(CancellationToken cancellationToken);
        Task<Shift?> GetShiftByIdAsync(int id, CancellationToken cancellationToken);
        Task<Shift> CreateShiftAsync(Shift shift, CancellationToken cancellationToken);
        Task<Shift?> UpdateShiftAsync(int id, Shift shift, CancellationToken cancellationToken);
        Task<bool> DeleteShiftAsync(int id, CancellationToken cancellationToken);
    }
}
