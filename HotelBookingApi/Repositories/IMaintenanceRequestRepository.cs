using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IMaintenanceRequestRepository
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllAsync();
        Task<MaintenanceRequest> GetByIdAsync(int id);
        Task<MaintenanceRequest> AddAsync(MaintenanceRequest request);
        Task<MaintenanceRequest> UpdateAsync(MaintenanceRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
