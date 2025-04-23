using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> GetAllAsync(CancellationToken cancellationToken);
        Task<Shift?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Shift> AddAsync(Shift shift, CancellationToken cancellationToken);
        Task<Shift> UpdateAsync(Shift shift, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
