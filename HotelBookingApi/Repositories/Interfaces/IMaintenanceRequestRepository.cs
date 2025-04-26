using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories.Interfaces
{
    public interface IMaintenanceRequestRepository
    {
        Task<IEnumerable<MaintenanceRequest>> GetAll(CancellationToken cancellationToken);
        Task<MaintenanceRequest?> GetById(int id, CancellationToken cancellationToken);
        Task<MaintenanceRequest> Add(MaintenanceRequest request, CancellationToken cancellationToken);
        Task<MaintenanceRequest> Update(MaintenanceRequest request, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
