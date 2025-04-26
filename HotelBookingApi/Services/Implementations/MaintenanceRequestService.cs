using FluentValidation;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class MaintenanceRequestService : IMaintenanceRequestService
    {
        private readonly IMaintenanceRequestRepository _repository;
        private readonly IValidator<MaintenanceRequest> _maintenanceRequestValidator;

        public MaintenanceRequestService(IMaintenanceRequestRepository repository, IValidator<MaintenanceRequest> maintenanceRequestValidator)
        {
            _repository = repository;
            _maintenanceRequestValidator = maintenanceRequestValidator;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetAllRequests(CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }

        public async Task<MaintenanceRequest?> GetRequestById(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetById(id, cancellationToken);
        }

        public async Task<MaintenanceRequest> CreateRequest(MaintenanceRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _maintenanceRequestValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await _repository.Add(request, cancellationToken);
        }

        public async Task<MaintenanceRequest?> UpdateRequest(int id, MaintenanceRequest request, CancellationToken cancellationToken)
        {
            var existingRequest = await _repository.GetById(id, cancellationToken);
            if (existingRequest == null) return null;

            existingRequest.Description = request.Description;
            existingRequest.Status = request.Status;
            existingRequest.UpdatedAt = DateTime.UtcNow;

            return await _repository.Update(existingRequest, cancellationToken);
        }

        public async Task<bool> DeleteRequest(int id, CancellationToken cancellationToken)
        {
            return await _repository.Delete(id, cancellationToken);
        }
    }
}
