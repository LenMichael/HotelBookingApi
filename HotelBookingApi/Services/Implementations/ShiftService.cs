using FluentValidation;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IValidator<Shift> _shiftValidator;

        public ShiftService(IShiftRepository shiftRepository, IValidator<Shift> shiftValidator)
        {
            _shiftRepository = shiftRepository;
            _shiftValidator = shiftValidator;
        }

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync(CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Shift?> GetShiftByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Shift> CreateShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            var validationResult = await _shiftValidator.ValidateAsync(shift, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await _shiftRepository.AddAsync(shift, cancellationToken);
        }

        public async Task<Shift?> UpdateShiftAsync(int id, Shift shift, CancellationToken cancellationToken)
        {
            var existingShift = await _shiftRepository.GetByIdAsync(id, cancellationToken);
            if (existingShift == null) return null;

            existingShift.StartTime = shift.StartTime;
            existingShift.EndTime = shift.EndTime;
            existingShift.Role = shift.Role;
            existingShift.EmployeeId = shift.EmployeeId;

            return await _shiftRepository.UpdateAsync(existingShift, cancellationToken);
        }

        public async Task<bool> DeleteShiftAsync(int id, CancellationToken cancellationToken)
        {
            return await _shiftRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
