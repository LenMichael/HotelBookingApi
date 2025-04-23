using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IMaintenanceRequestService
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllRequests(CancellationToken cancellationToken);
        Task<MaintenanceRequest?> GetRequestById(int id, CancellationToken cancellationToken);
        Task<MaintenanceRequest> CreateRequest(MaintenanceRequest request, CancellationToken cancellationToken);
        Task<MaintenanceRequest?> UpdateRequest(int id, MaintenanceRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteRequest(int id, CancellationToken cancellationToken);
    }
}
