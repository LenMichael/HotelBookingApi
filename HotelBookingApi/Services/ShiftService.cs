using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
        {
            return await _shiftRepository.GetAllAsync();
        }

        public async Task<Shift> GetShiftByIdAsync(int id)
        {
            return await _shiftRepository.GetByIdAsync(id);
        }

        public async Task<Shift> CreateShiftAsync(Shift shift)
        {
            return await _shiftRepository.AddAsync(shift);
        }

        public async Task<Shift> UpdateShiftAsync(int id, Shift shift)
        {
            var existingShift = await _shiftRepository.GetByIdAsync(id);
            if (existingShift == null) return null;

            existingShift.StartTime = shift.StartTime;
            existingShift.EndTime = shift.EndTime;
            existingShift.Role = shift.Role;
            existingShift.EmployeeId = shift.EmployeeId;

            return await _shiftRepository.UpdateAsync(existingShift);
        }

        public async Task<bool> DeleteShiftAsync(int id)
        {
            return await _shiftRepository.DeleteAsync(id);
        }
    }
}
