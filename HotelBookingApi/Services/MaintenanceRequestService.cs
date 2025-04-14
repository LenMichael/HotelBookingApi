using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class MaintenanceRequestService : IMaintenanceRequestService
    {
        private readonly IMaintenanceRequestRepository _repository;

        public MaintenanceRequestService(IMaintenanceRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetAllRequestsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MaintenanceRequest> GetRequestByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MaintenanceRequest> CreateRequestAsync(MaintenanceRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task<MaintenanceRequest> UpdateRequestAsync(int id, MaintenanceRequest request)
        {
            var existingRequest = await _repository.GetByIdAsync(id);
            if (existingRequest == null) return null;

            existingRequest.Description = request.Description;
            existingRequest.Status = request.Status;
            existingRequest.UpdatedAt = DateTime.UtcNow;

            return await _repository.UpdateAsync(existingRequest);
        }

        public async Task<bool> DeleteRequestAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
