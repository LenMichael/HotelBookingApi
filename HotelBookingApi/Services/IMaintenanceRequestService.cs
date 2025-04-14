using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IMaintenanceRequestService
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllRequestsAsync();
        Task<MaintenanceRequest> GetRequestByIdAsync(int id);
        Task<MaintenanceRequest> CreateRequestAsync(MaintenanceRequest request);
        Task<MaintenanceRequest> UpdateRequestAsync(int id, MaintenanceRequest request);
        Task<bool> DeleteRequestAsync(int id);
    }
}
